using ApplicationCore.Helpers;
using ApplicationCore.Models;
using ApplicationCore.Views;
using Infrastructure.Paging;
using Infrastructure.Views;
using Microsoft.AspNetCore.Identity;
using System;

namespace Web.Models;

public class UsersAdminRequest
{
   public UsersAdminRequest(bool active, string? role, string? keyword, int page = 1, int pageSize = 10)
   {
      Active = active;
      Role = role;
      Keyword = keyword;
      Page = page < 1 ? 1 : page;
      PageSize = pageSize;
   }
   public bool Active { get; set; }
   public string? Role { get; set; }
   public string? Keyword { get; set; }
   public int Page { get; set; } 
   public int PageSize { get; set; }
}
public class UsersAdminModel
{
   public UsersAdminModel(UsersAdminRequest request, ICollection<RoleViewModel> roles)
   {
      Request = request;
      Roles = roles;
   }

   public UsersAdminRequest Request { get; set; }

   public ICollection<RoleViewModel> Roles{ get; set; }

   
   
}

public class UsersImportRequest
{
   public List<IFormFile> Files { get; set; } = new List<IFormFile>();
}


public class UserAddRequest
{
   public string Name { get; set; } = String.Empty;

   public string UserName { get; set; } = String.Empty;

   public string Email { get; set; } = String.Empty;

   public string? PhoneNumber { get; set; }

   public string? Dob { get; set; }

}
public class UserEditRequest
{
   public string Email { get; set; } = String.Empty;

   public string? PhoneNumber { get; set; }

   public bool Active { get; set; }

}