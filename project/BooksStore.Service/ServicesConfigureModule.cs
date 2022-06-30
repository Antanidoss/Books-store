using BooksStore.Infrastructure.Implementation;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using BooksStore.Services.Profiles;
using BooksStore.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using BooksStore.Services.Implementation.Services.Base;
using BooksStore.Services.Interfaces.Services.Base;
using BooksStore.Services.Implementation.Services.WithCaching;
using BooksStore.Services.Interfaces.Services.WithCaching;

namespace BooksStore.Services
{
    public class ServicesConfigureModule : IDependenceModule, IMapperModule
    {
        public IServiceCollection RegisterDependencies(IServiceCollection services, IConfiguration Configuration)
        {
            //MemoryCache configuration
            services.AddMemoryCache();

            //CacheManager configuration
            services.AddTransient<ICacheManager, MemoryCacheManager>();
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();

            RegisterServices(services);
            RegisterCachingServices(services);

            return services;
        }

        public void RegisterMapperProfiles(IMapperConfigurationExpression mapperConfigure)
        {
            mapperConfigure.AddProfile(new AuthorDTOProfile());
            mapperConfigure.AddProfile(new BookDTOProfile());
            mapperConfigure.AddProfile(new CategoryDTOProfile());
            mapperConfigure.AddProfile(new BasketDTOProfile());
            mapperConfigure.AddProfile(new OrderDTOProfile());
            mapperConfigure.AddProfile(new AppUserDTOProfile());
            mapperConfigure.AddProfile(new RoleDTOProfile());
            mapperConfigure.AddProfile(new CommentDTOProfile());
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
        }

        private void RegisterCachingServices(IServiceCollection services)
        {
            services.AddScoped<IBookCachingService, BookCachingService>();
            services.AddScoped<IBasketCachingService, BasketCachingService>();
            services.AddScoped<ICommentCachingService, CommentCachingService>();
            services.AddScoped<IOrderCachingService, OrderCachingService>();
        }
    }
}