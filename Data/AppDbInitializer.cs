using Microsoft.AspNetCore.Identity;
using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Data
{
    public class AppDbInitializer
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager, UserManager<users> userManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Manager"));
            await roleManager.CreateAsync(new IdentityRole("Cashier"));
            users Admin = new users()
            {
                FirstName = "Admin",
                UserName = "Admin",
                Email = "Admin@gmail.com"
            };
            var result = await userManager.CreateAsync(Admin, "Admin");
            if (result.Succeeded)
            {
                var role = await userManager.AddToRoleAsync(Admin, "Manager");
            }
        }
    }
}
