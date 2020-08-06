using BooksStore.Core.AppUserModel;
using BooksStore.Infastructure.AuthorRep;
using BooksStore.Infastructure.BasketRep;
using BooksStore.Infastructure.BookRep;
using BooksStore.Infastructure.CategoryRep;
using BooksStore.Infastructure.CommentRep;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Infastructure.OrderRep;
using BooksStore.Service.AuthorSer;
using BooksStore.Service.BasketSer;
using BooksStore.Service.BookSer;
using BooksStore.Service.CategorySer;
using BooksStore.Service.CommentSer;
using BooksStore.Service.Interfaces;
using BooksStore.Service.OrderSer;
using BooksStore.Web.Converter._AppUser;
using BooksStore.Web.Converter._Book;
using BooksStore.Web.Converter._Category;
using BooksStore.Web.Converter._Comment;
using BooksStore.Web.Converter._Order;
using BooksStore.Web.Converter._Role;
using BooksStore.Web.Interface;
using BooksStore.Web.Interface.Converter;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Models.User.CurUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BooksStore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EFDbContext>(option => option.UseSqlServer(connection));

            services.AddAntiforgery();
            services.AddAuthentication();
            services.AddAuthorization();

            services.AddIdentity<AppUser, IdentityRole>(option => 
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 6;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<EFDbContext>().AddDefaultTokenProviders();
 
            services.AddScoped<ICurrentUser, CurrentUser>();

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddTransient<IAppUserConverter, AppUserConverter>();
            services.AddTransient<IBookConverter, BookConverter>();
            services.AddTransient<IOrderConverter, OrderConverter>();
            services.AddTransient<ICommentConverter, CommentConverter>();
            services.AddTransient<IRoleConverter, RoleConverter>();
            services.AddTransient<ICategoryConverter, CategoryConverter>();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Book}/{action=IndexBooks}/{id?}");
            });
        }
    }
}
