using FitnessClub.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CustomIdentityApp
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<ProjectUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "Admin@gmail.com";
            string password = "Admin1*";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("client") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("client"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                ProjectUser admin = new ProjectUser { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}