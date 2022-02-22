using BooksStore.Core.Entities;
using BooksStore.Infastructure.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BooksStore.Web.Сommon.Initializer
{
    public static class AppInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var categoryRepository = serviceProvider.GetRequiredService<ICategoryRepository>();
            var authorRepository = serviceProvider.GetRequiredService<IAuthorRepository>();

            await RoleInitializer.InitializeAsync(userManager, roleManager);
            await CategoryInitializer.InitializeAsync(categoryRepository);
            await AuthorInitializer.InitializeAsync(authorRepository);
        }
    }
}