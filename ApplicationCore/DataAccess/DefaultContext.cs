using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using ApplicationCore.Models.Auth;
using ApplicationCore.Models.TransExam;

namespace ApplicationCore.DataAccess;
public class DefaultContext : IdentityDbContext<User, Role, string,
        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
{
  
   public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
	{
      
   }
   protected override void OnModelCreating(ModelBuilder builder)
   {
      base.OnModelCreating(builder);
      builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

      if (Database.IsNpgsql())
      {
         var types = builder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?));
         foreach (var property in types)
         {
            property.SetColumnType("timestamp without time zone");
         }
      }
   }
   public DbSet<ModifyRecord> ModifyRecords => Set<ModifyRecord>();
   public DbSet<Profiles> Profiles => Set<Profiles>();

   public DbSet<Attachment> Attachments => Set<Attachment>();

   #region Exams
   public DbSet<Question> Questions => Set<Question>();
   public DbSet<Option> Options => Set<Option>();
   #endregion

   #region Auth
   public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
	public DbSet<OAuth> OAuth => Set<OAuth>();
   public DbSet<AuthToken> AuthTokens => Set<AuthToken>();
   #endregion

   public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();

}
