using BooksStore.Service.AuthorSer;
using BooksStore.Service.BasketSer;
using BooksStore.Service.BookSer;
using BooksStore.Service.CategorySer;
using BooksStore.Service.CommentSer;
using BooksStore.Service.Implementation.Identity;
using BooksStore.Service.Interfaces;
using BooksStore.Service.Interfaces.Identity;
using BooksStore.Service.OrderSer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMemoryCache();

            //Repositories configuration 
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }
    }
}
