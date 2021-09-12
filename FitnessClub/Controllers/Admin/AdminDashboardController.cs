using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using FitnessClub.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Controllers.Admin
{
    [Authorize]  //(Roles ="admin")
    public class AdminDashboardController : Controller
    {
        private readonly UserManager<ProjectUser> _userManager;
        private readonly SignInManager<ProjectUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminDashboardController(
            UserManager<ProjectUser> userManager,
            SignInManager<ProjectUser> signInManager,
            RoleManager<IdentityRole> roleManager
    )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            ViewData["CurrentUserName"] = currentUser.UserName;
            ViewData["CurrentUserAvatar"] = currentUser.Avatar;
            return View("~/Views/Admin/Index.cshtml");
        }
    }
}
