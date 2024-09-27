using ApplicationCore.Authorization;
using ApplicationCore.Consts;
using ApplicationCore.DataAccess;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models.TransExam;

namespace Web.Controllers.Tests;

public class ATestsController : BaseTestController
{
   private readonly UserManager<User> _userManager;
   private readonly IUsersService _usersService;
   private readonly IProfilesService _profilesService;
   public ATestsController(UserManager<User> userManager, IUsersService usersService, IProfilesService profilesService)
   {
      _userManager = userManager;
      _usersService = usersService;
      _profilesService = profilesService;
   }
   [HttpGet]
   public async Task<ActionResult> Index()
   {
      int option_count = 4;
      var model = new QuestionAddRequest();
      model.Options = new List<OptionAddRequest>();
      for (int i = 0; i < option_count; i++)
      {
         model.Options.Add(new OptionAddRequest());
      }
      return Ok(model);
   }


   [HttpGet("ex")]
   public ActionResult Ex()
   {
      throw new Exception("Test Throw Exception");
   }
}
