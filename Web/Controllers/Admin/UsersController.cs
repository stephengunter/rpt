using ApplicationCore.Services;
using ApplicationCore.Models;
using ApplicationCore.Helpers;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Infrastructure.Helpers;
using Web.Models;
using System.Text;
using ApplicationCore.Settings;
using Microsoft.Extensions.Options;
using ApplicationCore.Views;
using Microsoft.IdentityModel.Tokens;
using ApplicationCore.Authorization;
using Infrastructure.Paging;
using Azure.Core;
using ApplicationCore.Migrations;

namespace Web.Controllers.Admin;

public class UsersController : BaseAdminController
{
   private readonly IUsersService _usersService;
   private readonly IProfilesService _profilesService;
   private readonly IMapper _mapper;
   private readonly JudSettings _judSettings;
   private readonly AdminSettings _adminSettings;

   public UsersController(IUsersService usersService, IProfilesService profilesService, IOptions<JudSettings> judSettings,
      IOptions<AdminSettings> adminSettings, IMapper mapper)
   {
      _usersService = usersService;
      _judSettings = judSettings.Value;
      _mapper = mapper;
      _adminSettings = adminSettings.Value;
   }
   [HttpGet("init")]
   public async Task<ActionResult<UsersAdminModel>> Init()
   {
      bool active = true;
      string role = "";
      string keyword = "";
      int page = 1;
      int pageSize = 10;

      var request = new UsersAdminRequest(active, role, keyword, page, pageSize);

      var roles = _usersService.FetchRoles();

      return new UsersAdminModel(request, roles.MapViewModelList(_mapper));
   }

   [HttpGet]
   public async Task<ActionResult<PagedList<User, UserViewModel>>> Index(bool active, string? role, string? keyword, int page = 1, int pageSize = 10)
   {
      bool includeRoles = true;
      var roleNames = new List<string>();
      if(!string.IsNullOrEmpty(role)) roleNames = role.Split(',').ToList();
     
      IEnumerable<User> users;      
      if (roleNames.Count == 0)
      {
         users = await _usersService.FetchAllAsync(includeRoles);
      }
      else 
      {
         var selectedRoles = new List<Role>();
         foreach (var roleName in roleNames)
         {
            var selectedRole = await _usersService.FindRoleAsync(roleName);
            if (selectedRole == null) ModelState.AddModelError("role", $"Role '{roleName}' not found.");
            else selectedRoles.Add(selectedRole);
         }
         if (!ModelState.IsValid) return BadRequest(ModelState);

         users = await _usersService.FetchByRolesAsync(selectedRoles, includeRoles);
      }

      users = users.Where(u => u.Active == active);

      var keywords = keyword.GetKeywords();
      if (keywords.HasItems()) users = users.FilterByKeyword(keywords);

      return users.GetPagedList(_mapper, page, pageSize);
   }

   [HttpGet("create")]
   public ActionResult<UserAddRequest> Create() => new UserAddRequest();

   [HttpPost]
   public async Task<ActionResult<UserViewModel>> Store([FromBody] UserAddRequest model)
   {
      string name = await CheckNameAsync(model.Name, "");
      string userName = await CheckUserNameAsync(model.UserName, "");
      string phone = await CheckPhoneAsync(model.PhoneNumber!);
      string email = await CheckEmailAsync(model.Email, "");
      string dob = await CheckDobAsync(model.Dob!);

      if (!ModelState.IsValid) return BadRequest(ModelState);

      var user = new User()
      {
         UserName = userName,
         Email = email,
         Name = name,
         PhoneNumber = phone,
         CreatedAt = DateTime.Now,
         LastUpdated = DateTime.Now,
         CreatedBy = User.Id(),
         UpdatedBy = User.Id(),
         EmailConfirmed = true,
         PhoneNumberConfirmed = true,
         Active = true
      };

      user.Profiles = new Profiles
      {
         Name = user.Name,
         DateOfBirth = dob.ResolveToDate(),
         CreatedAt = DateTime.Now,
         LastUpdated = DateTime.Now,
         CreatedBy = User.Id(),
         UpdatedBy = User.Id()
      };

      user = await _usersService.CreateAsync(user);


      await _usersService.AddPasswordAsync(user, dob);

      return Ok(user.MapViewModel(_mapper));
   }

   [HttpGet("{id}")]
   public async Task<ActionResult<UserViewModel>> Details(string id)
   {
      var user = await _usersService.GetByIdAsync(id);
      if (user == null) return NotFound();

      string dp = user.GetDefaultPassword();
      if (!string.IsNullOrEmpty(dp))
      {
         bool isValid = await _usersService.CheckPasswordAsync(user, dp);
         if (!isValid) throw new Exception("CheckPassword Failed");
      }

      return user.MapViewModel(_mapper);
   }

   [HttpGet("edit/{id}")]
   public async Task<ActionResult<UserEditRequest>> Edit(string id)
   {
      var user = await _usersService.FindByIdAsync(id);
      if (user == null) return NotFound();

      var model = new UserEditRequest();
      user.SetValuesTo(model);
      return model;
   }
   [HttpPut("{id}")]
   public async Task<ActionResult> Update(string id, [FromBody] UserEditRequest model)
   {
      var user = await _usersService.FindByIdAsync(id);
      if (user == null) return NotFound();
      
      string phone = await CheckPhoneAsync(model.PhoneNumber!);
      string email = await CheckEmailAsync(model.Email, id);

      if (!ModelState.IsValid) return BadRequest(ModelState);

      user.PhoneNumber = phone;
      user.Email = email;
      user.Active = model.Active;

      await _usersService.UpdateAsync(user);

      return NoContent();
   }

   [HttpGet("roles")]
   public ActionResult<IEnumerable<RoleViewModel>> GetRoles()
   {
      var roles = _usersService.FetchRoles();
      return roles.MapViewModelList(_mapper);
   }
   [HttpPost("import")]
   public async Task<IActionResult> Import([FromForm] AdminFileRequest request)
   {
      ValidateRequest(request, _adminSettings);
      if (!ModelState.IsValid) return BadRequest(ModelState);

      if (request.Files.Count < 1)
      {
         ModelState.AddModelError("files", "�����W���ɮ�");
         return BadRequest(ModelState);
      }

      var file = request.Files.FirstOrDefault();
      if (Path.GetExtension(file!.FileName).ToLower() != ".txt")
      {
         ModelState.AddModelError("files", "�ɮ׮榡���~");
         return BadRequest(ModelState);
      }
      var users = new List<User>();
      var exLines = new List<string>();
      using (var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
      {
         while (reader.Peek() >= 0)
         {
            var line = await reader.ReadLineAsync();
            if (!String.IsNullOrEmpty(line))
            {
               string[] parts = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
               if (parts.Length == 3)
               {
                  string name = parts[0].Trim();
                  string type = parts[1].Trim();
                  string title = parts[2].Trim();
                  if (type == "�ϥΪ�")
                  {
                     users.Add(new User
                     {
                        UserName = name,
                        Name = title,
                        Email = $"{name}@{_judSettings.Domain}",
                        LastUpdated = DateTime.Now
                     });
                  }
                  else
                  {
                     exLines.Add(line);
                  }

               }
               else
               {
                  exLines.Add(line);
               }
            }
            
         }
           
      }

      foreach (var user in users) 
      {
         var existingUser = await _usersService.FindByUsernameAsync(user.UserName!);
         if (existingUser is null)
         {
            await _usersService.CreateAsync(user);
         }
         else
         {
            existingUser.Email = user.Email;
            existingUser.Name = user.Name;
            await _usersService.UpdateAsync(existingUser);
         }
      }

      return Ok(exLines);
   }

   Role? GetRole(UsersAdminRequest request, IEnumerable<Role> roles)
   {
      if (String.IsNullOrEmpty(request.Role)) return null;
      var role = roles.FirstOrDefault(role => request.Role.EqualTo(role.Name!));
      if (role is null)
      {
         ModelState.AddModelError("role", $"Role '{ request.Role }' not found.");
         return null;
      }
      return role;
   }

   async Task<string> CheckNameAsync(string input, string id)
   {
      if (String.IsNullOrEmpty(input))
      {
         ModelState.AddModelError("name", "������g�m�W");
         return "";
      }

      string name = input.Trim();

      var existingUser = await _usersService.FindByNameAsync(name);
      if (existingUser != null && existingUser.Id != id)
      {
         ModelState.AddModelError("name", "�m�W���ƤF");
         return "";
      }
      return name;
   }
   async Task<string> CheckUserNameAsync(string input, string id)
   {
      if(String.IsNullOrEmpty(input))
      {
         ModelState.AddModelError("userName", "������g�����Ҹ�");
         return "";
      }
      string username = input.Trim();
      if (!username.IsValidTWID())
      {
         ModelState.AddModelError("userName", "�����Ҹ����榡�����T");
         return "";
      }


      var existingUser = await _usersService.FindByUsernameAsync(username);
      if(existingUser != null && existingUser.Id != id)
      {
         ModelState.AddModelError("userName", "�����Ҹ����ƤF");
         return "";
      }
      return username;
   }
   async Task<string> CheckPhoneAsync(string input)
   {
      if (String.IsNullOrEmpty(input))
      {
         ModelState.AddModelError("phoneNumber", "������g������X");
         return "";
      }

      string phoneNumber = input.Trim();
      if (!phoneNumber.IsValidPhoneNumber())
      {
         ModelState.AddModelError("phoneNumber", "������X���榡�����T");
         return "";
      }
      return phoneNumber;
   }
   async Task<string> CheckEmailAsync(string input, string id)
   {
      if (String.IsNullOrEmpty(input))
      {
         ModelState.AddModelError("email", "������gemail");
         return "";
      }
      string email = input.Trim();
      if (!email.IsValidEmail())
      {
         ModelState.AddModelError("email", "email���榡�����T");
         return "";
      }

      var existingUser = await _usersService.FindByEmailAsync(email);
      if (existingUser != null && existingUser.Id != id)
      {
         ModelState.AddModelError("email", "email���ƤF");
         return "";
      }

      return email;
   }
   async Task<string> CheckDobAsync(string input)
   {
      if (String.IsNullOrEmpty(input))
      {
         ModelState.AddModelError("dob", "������g�X�ͤ��");
         return "";
      } 

      string dob = input.Trim();
      if (!dob.IsValidDob())
      {
         ModelState.AddModelError("dob", "�X�ͤ�����榡�����T");
         return "";
      }
      return dob;
   }

}