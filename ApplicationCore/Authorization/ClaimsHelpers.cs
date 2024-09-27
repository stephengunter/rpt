using Infrastructure.Helpers;
using System.Security.Claims;
using System.Security.Principal;
using ApplicationCore.Models;
using ApplicationCore.Consts;
using System.Data;


namespace ApplicationCore.Authorization;
public static class ClaimsHelpers
{
   public static string UserName(this ClaimsPrincipal user)
   {
      var claim = user.FindFirst(ClaimTypes.NameIdentifier);
      if (claim != null) return claim.Value;
      return user.Claims.Find(JwtClaimIdentifiers.Sub)?.Value ?? string.Empty;
   }

   public static string Id(this ClaimsPrincipal user)
   {
      var claim = user.FindFirst(JwtClaimIdentifiers.Id);
      if (claim != null) return claim.Value;
      return user.Claims.Find(JwtClaimIdentifiers.Id)?.Value ?? string.Empty;
   }

   public static IEnumerable<int> DepartmentIds(this ClaimsPrincipal user)
   {
      var claim = user.FindFirst(JwtClaimIdentifiers.Departments);
      if (claim != null) return claim.Value.SplitToIntList();
      return user.Claims.Find(JwtClaimIdentifiers.Departments)?.Value.SplitToIntList() ?? new List<int>();
   }

   public static IEnumerable<string> Roles(this ClaimsPrincipal user)
   {
      var claim = user.FindFirst(ClaimTypes.Role);
      if (claim != null) return claim.Value.SplitToList();
      return user.Claims.Find(JwtClaimIdentifiers.Roles)?.Value.SplitToList() ?? new List<string>();
   }
   public static bool HasRole(this ClaimsPrincipal user, AppRoles appRole)
      => Roles(user).IsNullOrEmpty() ? false : Roles(user).FirstOrDefault(r => r.EqualTo(appRole.ToString())) != null;
   public static OAuthProvider Provider(this ClaimsPrincipal user)
   {
      string providerName = user.Claims.Find(JwtClaimIdentifiers.Provider)?.Value ?? string.Empty;
      OAuthProvider provider;
      if (!Enum.TryParse(providerName, true, out provider)) return OAuthProvider.Unknown;
      return provider;
   }

   static Claim? Find(this IEnumerable<Claim> claims, string val)
         => claims.FirstOrDefault(c => c.Type.EqualTo(val));
   
}