﻿using BooksStore.Common.Interfaces;
using BooksStore.Core.Entities;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Implementation.Repositories;
using BooksStore.Services.Interfaces.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BooksStore.Infastructure
{
    public class InfrastructureConfigureModule : IDependenceModule, IDBConfigureModule
    {
        public IServiceCollection RegisterDependencies(IServiceCollection services, IConfiguration Configuration)
        {
            // Database context configuration 
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EFDbContext>(option => option.UseSqlServer(connection));

            //Identity configuration 
            services.AddIdentity<AppUser, IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 6;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<EFDbContext>().AddDefaultTokenProviders();

            //Repositories configuration 
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();

            return services;
        }

        public void ConfigureDB(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<EFDbContext>();
                if (!(context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                {
                    context.Database.EnsureCreated();
                }
            }
        }
    }
}