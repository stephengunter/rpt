﻿using ApplicationCore.DataAccess;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Helpers;
using ApplicationCore.Exceptions;
using ApplicationCore.Consts;
using System.Data;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Specifications;
using System.Linq;
using System.Numerics;

namespace ApplicationCore.Services;

public interface IUsersService
{
   #region Fetch
   Task<IEnumerable<User>> FetchAllAsync(bool includeRoles = false);
   Task<IEnumerable<User>> FetchByRolesAsync(IEnumerable<Role> roles, bool includeRoles = false);
   Task<IEnumerable<User>> FetchByIdsAsync(IEnumerable<string> ids, bool includeRoles = false);
   IEnumerable<Role> FetchRoles();
	#endregion

	#region Find
	Task<User?> FindByIdAsync(string id);
	Task<User?> FindByEmailAsync(string email);
   Task<User?> FindByNameAsync(string name);
   Task<User?> FindByUsernameAsync(string username);
   User? FindByPhone(string phone);
   Task<Role?> FindRoleAsync(string name);
   #endregion

   #region Get
   Task<User?> GetByIdAsync(string id, bool includeRoles = false);
   #endregion

   #region Store
   Task<User> CreateAsync(User user);
	Task UpdateAsync(User user);
	Task AddToRoleAsync(User user, string role);
   Task RemoveFromRoleAsync(User user, string role);
   #endregion

   #region Get
   Task<IList<string>> GetRolesAsync(User user);
	IEnumerable<Role> GetRolesByUserId(string userId);

   #endregion

   #region Check
   Task<bool> HasRoleAsync(User user, string role);
   Task<bool> IsAdminAsync(User user);
   Task<bool> HasPasswordAsync(User user);
   Task<bool> CheckPasswordAsync(User user, string password);

   Task<bool> AddPasswordAsync(User user, string password);
   Task<bool> ChangePasswordAsync(User user, string old_password, string new_password);
   #endregion
}

public class UsersService : IUsersService
{
	DefaultContext _context;
	private readonly UserManager<User> _userManager;
	private readonly RoleManager<Role> _roleManager;
   private readonly IDefaultRepository<User> _usersRepository;

   public UsersService(DefaultContext context, UserManager<User> userManager, RoleManager<Role> roleManager,
      IDefaultRepository<User> usersRepository)
   {
		_context = context;
		_userManager = userManager;
		_roleManager = roleManager;
      _usersRepository = usersRepository;
   }
	string DevRoleName = AppRoles.Dev.ToString();
	string BossRoleName = AppRoles.Boss.ToString();

   #region Fetch
   public async Task<IEnumerable<User>> FetchAllAsync(bool includeRoles = false)
    => await _usersRepository.ListAsync(new UsersSpecification(includeRoles));
   public async Task<IEnumerable<User>> FetchByRolesAsync(IEnumerable<Role> roles, bool includeRoles = false)
	{
		var users = await _usersRepository.ListAsync(new UsersSpecification(includeRoles));
		if (roles.IsNullOrEmpty()) return users;

		return FetchByRoles(users, roles);
   }
   public async Task<IEnumerable<User>> FetchByIdsAsync(IEnumerable<string> ids, bool includeRoles = false)
      => await _usersRepository.ListAsync(new UsersSpecification(ids, includeRoles));
   public IEnumerable<Role> FetchRoles() => _roleManager.Roles.ToList();
	#endregion

	#region Find
	public async Task<User?> FindByIdAsync(string id) => await _userManager.FindByIdAsync(id);
	public async Task<User?> FindByEmailAsync(string email) => await _userManager.FindByEmailAsync(email);
   public async Task<User?> FindByUsernameAsync(string username) => await _userManager.FindByNameAsync(username);
   public async Task<User?> FindByNameAsync(string name) => await _userManager.Users.FirstOrDefaultAsync(x => x.Name == name);
   public User? FindByPhone(string phone) => _userManager.Users.FirstOrDefault(x => x.PhoneNumber == phone);
   public async Task<Role?> FindRoleAsync(string name) => await _roleManager.FindByNameAsync(name);
	#endregion


	#region Get
	public async Task<User?> GetByIdAsync(string id, bool includeRoles = false)
       => await _usersRepository.FirstOrDefaultAsync(new UsersSpecification(id, includeRoles));
   #endregion

   #region Store
   public async Task<User> CreateAsync(User user)
	{
      var result = await _userManager.CreateAsync(user);
		if (result.Succeeded) return user;

		var error = result.Errors.FirstOrDefault();
		string msg = $"{error!.Code} : {error!.Description}" ?? string.Empty;

		throw new CreateUserException(user, msg);
	}

	public async Task UpdateAsync(User user)
	{
		var result = await _userManager.UpdateAsync(user);
		if(!result.Succeeded)
		{
			var error = result.Errors.FirstOrDefault();
			string msg = $"{error!.Code} : {error!.Description}" ?? string.Empty;

			throw new UpdateUserException(user, msg);
		}
	}

	public async Task AddToRoleAsync(User user, string role)
	{
      var result = await _userManager.AddToRoleAsync(user, role);
      if (!result.Succeeded)
      {
         var error = result.Errors.FirstOrDefault();
         string msg = $"{error!.Code} : {error!.Description}" ?? string.Empty;

         throw new UpdateUserRoleException(user, role, msg);
      }
   }
   public async Task RemoveFromRoleAsync(User user, string role)
   {
      var result = await _userManager.RemoveFromRoleAsync(user, role);
      if (!result.Succeeded)
      {
         var error = result.Errors.FirstOrDefault();
         string msg = $"{error!.Code} : {error!.Description}" ?? string.Empty;

         throw new UpdateUserRoleException(user, role, msg);
      }
   }

   #endregion

   #region Get
   public async Task<IList<string>> GetRolesAsync(User user) => await _userManager.GetRolesAsync(user);

	public IEnumerable<Role> GetRolesByUserId(string userId)
	{
		var userRoles = _context.UserRoles.Where(x => x.UserId == userId);
		var roleIds = userRoles.Select(ur => ur.RoleId);

		return _roleManager.Roles.Where(r => roleIds.Contains(r.Id));
	}

   #endregion

   #region Check
   public async Task<bool> HasRoleAsync(User user, string role) 
      => await _userManager.IsInRoleAsync(user, role);
   public async Task<bool> IsAdminAsync(User user)
	{
		var roles = await GetRolesAsync(user);
		if (roles.IsNullOrEmpty()) return false;

		var match = roles.Where(r => r.Equals(DevRoleName) || r.Equals(BossRoleName)).FirstOrDefault();

		return match != null;
	}
   public async Task<bool> HasPasswordAsync(User user)
      => await _userManager.HasPasswordAsync(user);

   public async Task<bool> CheckPasswordAsync(User user, string password)
      => await _userManager.CheckPasswordAsync(user, password);

   public async Task<bool> AddPasswordAsync(User user, string password)
   {
      

      var result = await _userManager.AddPasswordAsync(user, password);
      return result.Succeeded;
   }
   public async Task<bool> ChangePasswordAsync(User user, string old_password, string new_password)
   {
      var result = await _userManager.ChangePasswordAsync(user, old_password, new_password);
      return result.Succeeded;
   }
   #endregion

   #region Helper

   IEnumerable<User> FetchByRoles(IEnumerable<User> users, IEnumerable<Role> roles)
   {
      var roleIds = roles.Select(x => x.Id);
      var userIdsInRoles = _context.UserRoles.Where(x => roleIds.Contains(x.RoleId)).Select(b => b.UserId).Distinct().ToList();
      return users.Where(user => userIdsInRoles.Contains(user.Id));
   }
   #endregion








}
