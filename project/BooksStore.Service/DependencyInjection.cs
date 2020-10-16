using AutoMapper;
using BooksStore.Core.AppUserModel;
using BooksStore.Infrastructure.Implementation.CacheManager;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Service.AuthorSer;
using BooksStore.Service.BasketSer;
using BooksStore.Service.BookSer;
using BooksStore.Service.CategorySer;
using BooksStore.Service.CommentSer;
using BooksStore.Service.Implementation.Identity;
using BooksStore.Service.Interfaces;
using BooksStore.Service.Interfaces.Identity;
using BooksStore.Service.OrderSer;
using BooksStore.Service.Profiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            //MemoryCache configuration
            services.AddMemoryCache();
            //CacheManager configuration
            services.AddTransient<ICacheManager, MemoryCacheManager>();

            //Services configuration 
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            // Auto mapper configuration
            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    //DTO profiles
            //    mc.AddProfile(new AuthorDTOProfile());
            //    mc.AddProfile(new BookDTOProfile());
            //    mc.AddProfile(new CategoryDTOProfile());
            //    mc.AddProfile(new BasketDTOProfile());
            //    mc.AddProfile(new OrderDTOProfile());
            //    mc.AddProfile(new AppUserDTOProfile());
            //    mc.AddProfile(new RoleDTOProfile());
            //    mc.AddProfile(new CommentDTOProfile());                
            //});
            //IMapper mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);
            
            return services;
        }
    }
}
