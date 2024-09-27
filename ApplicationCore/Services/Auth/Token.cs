using ApplicationCore.DataAccess;
using ApplicationCore.Consts;
using ApplicationCore.Models.Auth;
using ApplicationCore.Specifications.Auth;
using System.Net;
using Infrastructure.Helpers;

namespace ApplicationCore.Services.Auth;

public interface IAuthTokenService
{
   Task<AuthToken> CreateAsync(AuthToken model, int minutes);
   Task<AuthToken> CreateAsync(string username, AuthProvider provider, string ipAddress, string json, int minutes);
   Task<AuthToken?> CheckAsync(string token, string username, AuthProvider provider);
}

public class AuthTokenService : IAuthTokenService
{
   private readonly IDefaultRepository<AuthToken> _repository;

   public AuthTokenService(IDefaultRepository<AuthToken> repository)
   {
      _repository = repository;
   }
   public async Task<AuthToken> CreateAsync(AuthToken model, int minutes)
   {
      var expires = DateTime.Now.AddMinutes(minutes > 0 ? minutes : 5);
      model.Token = Guid.NewGuid().ToString();
      model.Expires = expires;
      model.LastUpdated = DateTime.Now;

      var exist = await FindAsync(model.UserName, model.Provider);
      if (exist != null)
      {
         var except = new List<string>() { "Id" };
         model.SetValuesTo(exist, except);

         await _repository.UpdateAsync(exist);
         return exist;
      }

      return await _repository.AddAsync(model);
   }

   public async Task<AuthToken> CreateAsync(string username, AuthProvider provider, string ipAddress, string json, int minutes)
   {
      var authToken = new AuthToken
      {
         UserName = username,
         Provider = provider,

         AdListJson = json,
         RemoteIpAddress = ipAddress
      };
      return await CreateAsync(authToken, minutes);
   }


   public async Task<AuthToken?> CheckAsync(string token, string username, AuthProvider provider)
   {
      var entity = await FindAsync(username, provider);
      if (entity == null) return null;

      if (entity.Token == token && entity.Active) return entity;
      return null;
   }

   async Task<AuthToken?> FindAsync(string username, AuthProvider provider)
      => await _repository.FirstOrDefaultAsync(new AuthTokenSpecification(username, provider));
}
