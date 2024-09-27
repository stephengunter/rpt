using Serilog;
using Serilog.Events;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.DataAccess;
using Microsoft.AspNetCore.Identity;
using ApplicationCore.Models;
using ApplicationCore.Settings;
using ApplicationCore.Consts;
using ApplicationCore.DI;
using Web.Filters;
using System.Text.Json.Serialization;
using Infrastructure.Helpers;
using QuestPDF.Infrastructure;
using Microsoft.AspNetCore.Http.Features;

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.CreateBootstrapLogger();

try
{
	Log.Information("Starting web application");
	var builder = WebApplication.CreateBuilder(args);
	var Configuration = builder.Configuration;
	builder.Host.UseSerilog((context, services, configuration) => configuration
			.ReadFrom.Configuration(context.Configuration)
			.ReadFrom.Services(services)
			.Enrich.FromLogContext());

	#region Autofac
	builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
	builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
	{
		builder.RegisterModule<ApplicationCoreModule>();
	});
   #endregion

   #region Add Configurations
   builder.Services.Configure<DbSettings>(Configuration.GetSection(SettingsKeys.Db));
   builder.Services.Configure<AppSettings>(Configuration.GetSection(SettingsKeys.App));
	builder.Services.Configure<AdminSettings>(Configuration.GetSection(SettingsKeys.Admin));
	builder.Services.Configure<AuthSettings>(Configuration.GetSection(SettingsKeys.Auth));
   builder.Services.Configure<AttachmentSettings>(Configuration.GetSection(SettingsKeys.Attachment));
   builder.Services.Configure<CompanySettings>(Configuration.GetSection(SettingsKeys.Company));
   builder.Services.Configure<MailSettings>(Configuration.GetSection(SettingsKeys.Mail));
   builder.Services.Configure<JudSettings>(Configuration.GetSection(SettingsKeys.Judicial));
   builder.Services.Configure<Jud3Settings>(Configuration.GetSection(SettingsKeys.Jud3));
   #endregion

   string connectionString = Configuration.GetConnectionString("Default")!;
	bool usePostgreSql = Configuration[$"{SettingsKeys.Db}:Provider"].EqualTo(DbProvider.PostgreSql);
   if (usePostgreSql)
	{
      builder.Services.AddDbContext<DefaultContext>(options =>
                  options.UseNpgsql(connectionString));
   }
	else
	{
      builder.Services.AddDbContext<DefaultContext>(options =>
                  options.UseSqlServer(connectionString));
   }
	

   #region AddIdentity
   builder.Services.AddIdentity<User, Role>(options =>
	{
      options.User.RequireUniqueEmail = false;

      options.Password.RequireDigit = false;
      options.Password.RequiredLength = 6;
      options.Password.RequireLowercase = false;
      options.Password.RequireNonAlphanumeric = false;
      options.Password.RequireUppercase = false;

   })
	.AddEntityFrameworkStores<DefaultContext>()
   .AddDefaultTokenProviders();
   #endregion

   #region AddFilters
   builder.Services.AddScoped<DevelopmentOnlyFilter>();
   #endregion

   builder.Services.AddCorsPolicy(Configuration);
   builder.Services.AddJwtBearer(Configuration);   
   builder.Services.AddAuthorizationPolicy();
   
   builder.Services.AddDtoMapper();
   builder.Services.AddControllers().AddJsonOptions(options => {
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });


	builder.Services.AddSwagger(Configuration);

   int maxFileSizeInMB = Configuration[$"{SettingsKeys.JudgebookFile}:MaxFileSize"]!.ToInt();
	if (maxFileSizeInMB < 1) maxFileSizeInMB = 100;
   builder.Services.Configure<FormOptions>(options =>
	{
		options.MultipartBodyLengthLimit = maxFileSizeInMB * 1024 * 1024;
   });

   QuestPDF.Settings.License = LicenseType.Community;

   var app = builder.Build();

   app.UseDefaultFiles();
   app.UseStaticFiles();

   if (usePostgreSql) 
	{
      AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
   }
	

	app.UseSerilogRequestLogging(); 

	if (app.Environment.IsDevelopment())
	{
		if(Configuration[$"{SettingsKeys.Developing}:SeedDatabase"].ToBoolean())
		{
			// Seed Database
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					await SeedData.EnsureSeedData(services, Configuration);
				}
				catch (Exception ex)
				{
					Log.Fatal(ex, "SeedData Error");
				}
			}
		}
		
		app.UseSwagger();
		app.UseSwaggerUI();
	}
	else
	{
		app.UseHttpsRedirection();
	}

   app.UseCors();
   app.UseAuthentication();
   app.UseAuthorization();

	app.MapControllers();
   app.MapFallbackToFile("/index.html");
   app.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
	Log.Information("finally");
	Log.CloseAndFlush();
}






