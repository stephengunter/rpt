using ApplicationCore.Helpers;
using ApplicationCore.Models;
using ApplicationCore.Views;
using Infrastructure.Paging;
using Infrastructure.Views;
using Microsoft.AspNetCore.Identity;
using System;

namespace Web.Models;

public class ProfilesEditRequest
{
   public string Name { get; set; } = String.Empty;

   public string Ps { get; set; } = String.Empty;

   public string? Dob { get; set; }

}

public class ProfilesCreateRequest : ProfilesEditRequest
{
   public string UserId { get; set; } = String.Empty;

}