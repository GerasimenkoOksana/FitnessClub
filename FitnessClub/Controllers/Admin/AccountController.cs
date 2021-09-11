using FitnessClub.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FitnessClub.Controllers.Admin
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Roles
        public IActionResult RoleIndex()
        {
            return View("~/Views/Admin/Account/RoleIndex.cshtml", _roleManager.Roles.ToList());
        }

        public IActionResult RoleCreate()
        {
            return View("~/Views/Admin/Account/RoleCreate.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleIndex");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }
        #endregion
        public class RegisterUserModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            public List<IdentityRole> Roles { get; set; }
        }
        private readonly UserManager<ProjectUser> _userManager;
        private readonly SignInManager<ProjectUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _environment;
        public AccountController(
                    UserManager<ProjectUser> userManager, 
                    SignInManager<ProjectUser> signInManager,
                    RoleManager<IdentityRole> roleManager,
                    IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _environment = environment;
        }

        #region Users
        public IActionResult Index()
        {
            ViewBag.defaultAvatar = "\\storage\\avatars\\unnamed.jpg";
            return View("~/Views/Admin/Account/Index.cshtml", _userManager.Users);
        }

        public IActionResult Create()
        {
            ViewBag.allRoles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View("~/Views/Admin/Account/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Email, string Password, string[] addRoles, IFormFile AvatarFile)
        {
            if (ModelState.IsValid)
            {
                ProjectUser user = new ProjectUser { Email = Email, UserName = Email};
                var wwwRootPath = _environment.WebRootPath;
                var fileName = Path.GetRandomFileName().Replace('.', '_')
                     + Path.GetExtension(AvatarFile.FileName);
                var filePath = Path.Combine(wwwRootPath + "\\storage\\avatars\\", fileName);  

                user.Avatar = "/storage/avatars/" + fileName; 

                using (var stream = System.IO.File.Create(filePath))
                {
                    await AvatarFile.CopyToAsync(stream);
                }
                var result = await _userManager.CreateAsync(user, Password);
                if (result.Succeeded)
                {                     
                    await _userManager.AddToRolesAsync(user, addRoles);                   
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View("~/Views/Admin/Account/Create.cshtml");
        }
        #endregion
    }
}
