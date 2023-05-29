using Microsoft.AspNetCore.Identity;

namespace Supermarket_Managment_System.Data
{
    public class AppDbInitializer
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("User"));
        }
    }
}
