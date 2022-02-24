using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Implementation;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Service.Implementation;
using BooksStore.Service.Implementation.Filters.BookFilters;
using BooksStore.Service.Implementation.Services;
using BooksStore.Service.Interfaces.Filter;
using BooksStore.Services.AuthorSer;
using BooksStore.Services.Implementation.IdentityServices;
using BooksStore.Services.Interfaces;
using BooksStore.Services.Interfaces.IdentityServices;
using Microsoft.Extensions.DependencyInjection;

namespace BooksStore.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            //MemoryCache configuration
            services.AddMemoryCache();
            //CacheManager configuration
            services.AddTransient<ICacheManager, MemoryCacheManager>();

            services.AddScoped<IRepositoryFactory, RepositoryFactory>();

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