﻿using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Implementation;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.Implementation;
using BooksStore.Services.Implementation.Filters.BookFilters;
using BooksStore.Services.Implementation.Services;
using BooksStore.Services.Interfaces.Filter;
using BooksStore.Services.AuthorSer;
using BooksStore.Services.Implementation.IdentityServices;
using BooksStore.Services.Interfaces;
using BooksStore.Services.Interfaces.IdentityServices;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using BooksStore.Services.Profiles;

namespace BooksStore.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services, IMapperConfigurationExpression mapperConfigure)
        {
            //MemoryCache configuration
            services.AddMemoryCache();

            //CacheManager configuration
            services.AddTransient<ICacheManager, MemoryCacheManager>();

            services.AddScoped<IRepositoryFactory, RepositoryFactory>();

            mapperConfigure.AddProfile(new AuthorDTOProfile());
            mapperConfigure.AddProfile(new BookDTOProfile());
            mapperConfigure.AddProfile(new CategoryDTOProfile());
            mapperConfigure.AddProfile(new BasketDTOProfile());
            mapperConfigure.AddProfile(new OrderDTOProfile());
            mapperConfigure.AddProfile(new AppUserDTOProfile());
            mapperConfigure.AddProfile(new RoleDTOProfile());
            mapperConfigure.AddProfile(new CommentDTOProfile());

            //Services configuration 
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<ISpecificationFilterBuilder<Book>, BookFilterBuilder>();
            
            return services;
        }
    }
}