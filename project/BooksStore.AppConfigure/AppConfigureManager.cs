﻿using BooksStore.AppConfigure.Models;
using BooksStore.Core.Entities;
using BooksStore.Infastructure;
using BooksStore.Infastructure.Interfaces.Repositories;
using BooksStore.Services;
using BooksStore.Web.Сommon.Initializer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BooksStore.AppConfigure
{
    public static class AppConfigureManager
    {
        public static void RegisterDependencies(BaseConfigureModel configureModel)
        {
            ConfigureInfrastructure(configureModel);
            ConfigureServices(configureModel);
        }

        public async static Task CreateDBIfNotExistAsync(IApplicationBuilder app)
        {
            new InfrastructureConfigureModule().ConfigureDB(app);

            var serviceProvider = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;

            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var categoryRepository = serviceProvider.GetRequiredService<ICategoryRepository>();
            var authorRepository = serviceProvider.GetRequiredService<IAuthorRepository>();

            await RoleInitializer.InitializeAsync(userManager, roleManager);
            await CategoryInitializer.InitializeAsync(categoryRepository);
            await AuthorInitializer.InitializeAsync(authorRepository);
        }

        private static void ConfigureInfrastructure(BaseConfigureModel configureModel)
        {
            new InfrastructureConfigureModule().RegisterDependencies(configureModel.Services, configureModel.Configuration);
        }

        private static void ConfigureServices(BaseConfigureModel configureModel)
        {
            var servicesConfigureModule = new ServicesConfigureModule();

            servicesConfigureModule.RegisterDependencies(configureModel.Services, configureModel.Configuration);
            servicesConfigureModule.RegisterMapperProfiles(configureModel.MapperConfigure);
        }
    }
}
