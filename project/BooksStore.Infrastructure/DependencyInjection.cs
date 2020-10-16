using BooksStore.Core.AppUserModel;
using BooksStore.Infastructure.AuthorRep;
using BooksStore.Infastructure.BasketRep;
using BooksStore.Infastructure.BookRep;
using BooksStore.Infastructure.CategoryRep;
using BooksStore.Infastructure.CommentRep;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Infastructure.OrderRep;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BooksStore.Infastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfastructure(this IServiceCollection services, IConfiguration Configuration)
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

            return services;
        }
    }
}
