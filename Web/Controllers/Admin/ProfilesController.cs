using ApplicationCore.Services;
using ApplicationCore.Models;
using ApplicationCore.Views;
using ApplicationCore.Helpers;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ApplicationCore.DtoMapper;
using ApplicationCore.Authorization;
using Infrastructure.Helpers;
using Web.Models;

namespace Web.Controllers.Admin;

public class ProfilesController : BaseAdminController
{
   private readonly IUsersService _usersService;
   private readonly IProfilesService _profilesService;
   private readonly IMapper _mapper;
  
   public ProfilesController(IUsersService usersService, IProfilesService profilesService, IMapper mapper)
   {
      _usersService = usersService;
      _profilesService = profilesService;
      _mapper = mapper;
   }

   [HttpGet]
   public async Task<ActionResult<IEnumerable<ProfilesViewModel>>> Fetch()
   {
      var profiles = await _profilesService.FetchAsync();

      return profiles.MapViewModelList(_mapper);
   }

   [HttpGet("create/{id}")]
   public async Task<ActionResult<ProfilesCreateRequest>> Create(string id)
   {
      var user = await _usersService.FindByIdAsync(id);
      if (user == null) return NotFound();

      var profiles = await _profilesService.FindAsync(user);
      if (profiles != null) return NotFound();

      return new ProfilesCreateRequest() { UserId = id };
   }

   [HttpPost]
   public async Task<ActionResult<ProfilesViewModel>> Store([FromBody] ProfilesCreateRequest model)
   {
      string dob = await CheckDobAsync(model.Dob!);
      if (!ModelState.IsValid) return BadRequest(ModelState);

      var profiles = new Profiles();
      model.SetValuesTo(profiles);
      profiles.LastUpdated = DateTime.Now;
      profiles.UpdatedBy = User.Id();

      profiles.DateOfBirth = dob.ResolveToDate();
      await _profilesService.CreateAsync(profiles);

      var user = await _usersService.FindByIdAsync(profiles.UserId);
      if (!await _usersService.HasPasswordAsync(user!))
      {
         await _usersService.AddPasswordAsync(user!, dob);
      }

      return Ok(profiles.MapViewModel(_mapper));
   }

   [HttpGet("edit/{id}")]
   public async Task<ActionResult<ProfilesEditRequest>> Edit(string id)
   {
      var profiles = await _profilesService.FindAsync(new User { Id = id });
      if (profiles == null) return NotFound();

      var model = new ProfilesEditRequest();
      profiles.SetValuesTo(model);
      model.Dob = profiles.DateOfBirth.HasValue ? Convert.ToDateTime(profiles.DateOfBirth.Value).GetDateString(roc: true) : "";

      return Ok(model);
   }

   [HttpPut("{id}")]
   public async Task<ActionResult> Update(string id, [FromBody] ProfilesEditRequest model)
   {
      var profiles = await _profilesService.FindAsync(new User { Id = id });
      if (profiles == null) return NotFound();

      string dob = await CheckDobAsync(model.Dob!);
      if (!ModelState.IsValid) return BadRequest(ModelState);

      model.SetValuesTo(profiles);
      profiles.LastUpdated = DateTime.Now;
      profiles.UpdatedBy = User.Id();

      if (profiles.DateOfBirth.HasValue)
      {
         string current_password = Convert.ToDateTime(profiles.DateOfBirth.Value).GetDateString(roc: true);
         if (current_password != model.Dob)
         {
            profiles.DateOfBirth = dob.ResolveToDate();
            await _profilesService.UpdateAsync(profiles);

            var user = await _usersService.FindByIdAsync(id);
            if (await _usersService.HasPasswordAsync(user!))
            {
               await _usersService.ChangePasswordAsync(user!, current_password, dob);
            }
            else
            {
               await _usersService.AddPasswordAsync(user!, dob);
            }
         }
         else await _profilesService.UpdateAsync(profiles);
      }
      else
      {
         profiles.DateOfBirth = dob.ResolveToDate();
         await _profilesService.UpdateAsync(profiles);

         var user = await _usersService.FindByIdAsync(id);
         if (!await _usersService.HasPasswordAsync(user!))
         {
            await _usersService.AddPasswordAsync(user!, dob);
         }
      }
      

      return NoContent();
   }

   [HttpDelete("{id}")]
   public async Task<IActionResult> Delete(string id)
   {
      var profiles = await _profilesService.FindAsync(new User { Id = id });
      if (profiles == null) return NotFound();
      
      await _profilesService.DeleteAsync(profiles);

      return NoContent();
   }
   async Task<string> CheckDobAsync(string input)
   {
      if (String.IsNullOrEmpty(input))
      {
         ModelState.AddModelError("dob", "必須填寫出生日期");
         return "";
      }

      string dob = input.Trim();
      if (!dob.IsValidDob())
      {
         ModelState.AddModelError("dob", "出生日期的格式不正確");
         return "";
      }
      return dob;
   }

}