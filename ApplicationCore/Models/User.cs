﻿using ApplicationCore.Models.Auth;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Models;

public class User : IdentityUser, IAggregateRoot, IBaseRecord
{	
	public string Name { get; set; } = String.Empty;
   public DateTime CreatedAt { get; set; } = DateTime.Now;
   public string CreatedBy { get; set; } = string.Empty;
   public DateTime? LastUpdated { get; set; }
   public string? UpdatedBy { get; set; }
   public bool Active { get; set; }

   public virtual RefreshToken? RefreshToken { get; set; }
   public virtual Profiles? Profiles { get; set; }

   
   public virtual ICollection<OAuth>? OAuthList { get; set; }
   public virtual ICollection<UserRole>? UserRoles { get; set; }

   public object GetId() => Id;
}

public class Role : IdentityRole
{
   public string Title { get; set; } = String.Empty;
   public virtual ICollection<UserRole>? UserRoles { get; set; }
}

public class UserRole : IdentityUserRole<string>
{
   [Required]
   public virtual User? User { get; set; }
   [Required]
   public virtual Role? Role { get; set; }
}
