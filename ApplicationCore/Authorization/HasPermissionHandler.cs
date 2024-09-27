using Microsoft.AspNetCore.Authorization;
using ApplicationCore.Consts;
using System.Security.Claims;
using ApplicationCore.Models;

namespace ApplicationCore.Authorization;

public class HasPermissionHandler : AuthorizationHandler<HasPermissionRequirement>
{
   bool HasAdminPermission(ClaimsPrincipal user)
      => user.HasRole(AppRoles.Boss) || user.HasRole(AppRoles.Dev) || user.HasRole(AppRoles.IT);

   bool HasJudgebookFilesPermission(ClaimsPrincipal user)
      => user.HasRole(AppRoles.Files) || user.HasRole(AppRoles.ChiefClerk) || user.HasRole(AppRoles.Clerk)
      || user.HasRole(AppRoles.Clerk) || user.HasRole(AppRoles.Recorder);
   protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionRequirement requirement)
   {
      if (requirement.Permission == Permissions.Admin)
      {
         if (HasAdminPermission(context.User))
         {
            context.Succeed(requirement);
            return Task.CompletedTask;
         }
      }
      else if (requirement.Permission == Permissions.JudgebookFiles)
      {
         if (HasAdminPermission(context.User))
         {
            context.Succeed(requirement);
            return Task.CompletedTask;
         }

         if (HasJudgebookFilesPermission(context.User))
         {
            context.Succeed(requirement);
            return Task.CompletedTask;
         }
      }

      context.Fail();
      return Task.CompletedTask;
   }


}
