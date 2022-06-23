using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Implementation;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.Implementation;
using BooksStore.Services.Implementation.Services;
using BooksStore.Services.AuthorSer;
using BooksStore.Services.Implementation.IdentityServices;
using BooksStore.Services.Interfaces;
using BooksStore.Services.Interfaces.IdentityServices;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using BooksStore.Services.Profiles;
using BooksStore.Common.Interfaces;
using Microsoft.Extensions.Configuration;

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
    }
}