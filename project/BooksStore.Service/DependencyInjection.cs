using BooksStore.Infrastructure.Implementation;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.AuthorSer;
using BooksStore.Services.Implementation.Identity;
using BooksStore.Services.Interfaces;
using BooksStore.Services.Interfaces.Identity;
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

            //Services configuration 
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
