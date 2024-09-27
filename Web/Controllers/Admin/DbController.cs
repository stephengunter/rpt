using ApplicationCore.Exceptions;
using ApplicationCore.Services;
using ApplicationCore.Settings;
using ApplicationCore.Views;
using ApplicationCore.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AutoMapper;
using ApplicationCore.DtoMapper;
using Web.Filters;
using Web.Models;
using ApplicationCore.Consts;
using ApplicationCore.Models;
using Infrastructure.Helpers;
using ApplicationCore.DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers.Admin;

public class DBController : BaseAdminController
{
   private readonly AdminSettings _adminSettings;
   private readonly DefaultContext _context;

   public DBController(IOptions<AdminSettings> adminSettings, DefaultContext context)
   {
      _adminSettings = adminSettings.Value;
      _context = context;
   }

   #region Properties

   
   string ConnectionString => _context.Database.GetDbConnection().ConnectionString;
   string DbName => new SqlConnectionStringBuilder(ConnectionString).InitialCatalog;



   string _backupFolder;
   string BackupFolder
   {
      get
      {
         if (String.IsNullOrEmpty(_backupFolder))
         {
            var path = Path.Combine(_adminSettings.BackupPath, DateTime.Today.ToDateNumber().ToString());
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            _backupFolder = path;
         }
         return _backupFolder;
      }
   }
   #endregion

   [HttpGet("dbname")]
   public ActionResult DBName() => Ok(DbName);

   [HttpPost("migrate")]
   public ActionResult Migrate([FromBody] AdminRequest model)
   {

      ValidateRequest(model, _adminSettings);
      if (!ModelState.IsValid) return BadRequest(ModelState);

      _context.Database.Migrate();

      return Ok();
   }
   [HttpPost("backup")]
   public ActionResult Backup([FromBody] AdminRequest model)
   {
      ValidateRequest(model, _adminSettings);
      if (!ModelState.IsValid) return BadRequest(ModelState);

      var fileName = Path.Combine(BackupFolder, $"{DbName}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.bak");

      string cmdText = $"BACKUP DATABASE [{DbName}] TO DISK = '{fileName}'";
      using (var conn = new SqlConnection(ConnectionString))
      {
         conn.Open();
         using (SqlCommand cmd = new SqlCommand(cmdText, conn))
         {
            int result = cmd.ExecuteNonQuery();

         }
         conn.Close();
      }

      return Ok();
   }


}