using FitnessClub.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Controllers.Admin
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ProjectUser> _userManager;
        private readonly SignInManager<ProjectUser> _signInManager;
        public AccountController(UserManager<ProjectUser> userManager, SignInManager<ProjectUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View("~/Views/Admin/Account/Index.cshtml", _userManager.Users);
        }
    }
}
