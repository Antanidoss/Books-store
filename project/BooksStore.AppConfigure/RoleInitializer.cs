using BooksStore.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BooksStore.Web.Сommon.Initializer
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "user" });
            }
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "admin" });
            }
            if (await userManager.FindByEmailAsync("adminBooksStore@gmail.com") == null)
            {
                string name = "AdminBooksStore";
                string email = "adminBooksStore@gmail.com";
                string password = "adminBooksStore123#";

                AppUser appUser = new AppUser()
                {
                    UserName = name,
                    Email = email
                };

                var result = await userManager.CreateAsync(appUser, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(appUser, "admin");
                }
            }
        }
    }
}
