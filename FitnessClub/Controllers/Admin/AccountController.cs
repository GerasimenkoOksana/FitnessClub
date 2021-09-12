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
using FitnessClub.Data;

namespace FitnessClub.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        #region Roles
        public async Task<IActionResult> RoleIndex()
        {
            var currentUser =await _userManager.GetUserAsync(User);
            ViewData["CurrentUserName"] = currentUser.UserName;
            ViewData["CurrentUserAvatar"] = currentUser.Avatar;
            return View("~/Views/Admin/Account/RoleIndex.cshtml", _roleManager.Roles.ToList());
        }

        public async Task<IActionResult> RoleCreate()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            ViewData["CurrentUserName"] = currentUser.UserName;
            ViewData["CurrentUserAvatar"] = currentUser.Avatar;
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
            var currentUser = await _userManager.GetUserAsync(User);
            ViewData["CurrentUserName"] = currentUser.UserName;
            ViewData["CurrentUserAvatar"] = currentUser.Avatar;
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
            [Display(Name = "Id")]
            public string Id { get; set; }

            [Required]
            [Display(Name = "UserName")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "FullName")]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Birthday")]
            public DateTime Birthday { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Day of registraton")]
            public DateTime Moment { get; set; }

            [Required]            
            [Display(Name = "Sex")]
            public Sex Sex { get; set; }

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
        public async Task<IActionResult> Index()
        {
            ViewBag.defaultAvatar = "\\storage\\avatars\\unnamed.jpg";
            var currentUser = await _userManager.GetUserAsync(User);
            ViewData["CurrentUserName"] = currentUser.UserName;
            ViewData["CurrentUserAvatar"] = currentUser.Avatar;
            return View("~/Views/Admin/Account/Index.cshtml", _userManager.Users);
        }

        public async Task<IActionResult> Create()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            ViewData["CurrentUserName"] = currentUser.UserName;
            ViewData["CurrentUserAvatar"] = currentUser.Avatar;
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
            var currentUser = await _userManager.GetUserAsync(User);
            ViewData["CurrentUserName"] = currentUser.UserName;
            ViewData["CurrentUserAvatar"] = currentUser.Avatar;
            return View("~/Views/Admin/Account/Create.cshtml");
        }

        // GET: Sports/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);               
            if (user == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/Account/Delete.cshtml",user);
        }

        // POST: Sports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
          //  await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}
