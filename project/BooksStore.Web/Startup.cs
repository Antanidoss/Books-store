using AutoMapper;
using BooksStore.Core.AppUserModel;
using BooksStore.Infastructure.AuthorRep;
using BooksStore.Infastructure.BasketRep;
using BooksStore.Infastructure.BookRep;
using BooksStore.Infastructure.CategoryRep;
using BooksStore.Infastructure.CommentRep;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Infastructure.OrderRep;
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
using BooksStore.Web.Interfaces;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Models.CurUser;
using BooksStore.Web.Models.Managers;
using BooksStore.Web.Profiles;
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

            // Database context configuration 
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EFDbContext>(option => option.UseSqlServer(connection));

            //Antiforgery configuration 
            services.AddAntiforgery();

            // Authentication and Authorization configuration 
            services.AddAuthentication();
            services.AddAuthorization();

            // Auto mapper configuration
            var mappingConfig = new MapperConfiguration(mc =>
            {
                //DTO profiles
                mc.AddProfile(new AuthorDTOProfile());
                mc.AddProfile(new BookDTOProfile());
                mc.AddProfile(new CategoryDTOProfile());
                mc.AddProfile(new BasketDTOProfile());
                mc.AddProfile(new OrderDTOProfile());
                mc.AddProfile(new AppUserDTOProfile());
                mc.AddProfile(new RoleDTOProfile());
                mc.AddProfile(new CommentDTOProfile());
                //View model profiles
                mc.AddProfile(new AppUserVMProfile());
                mc.AddProfile(new BookVMProfile());
                mc.AddProfile(new CommentVMProfile());
                mc.AddProfile(new CategoryVMProfile());
                mc.AddProfile(new RoleVMProfile());
                mc.AddProfile(new OrderVMProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Identity configuration 
            services.AddIdentity<AppUser, IdentityRole>(option => 
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 6;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<EFDbContext>().AddDefaultTokenProviders();


            //Current user configuration 
            services.AddScoped<ICurrentUser, CurrentUser>();

            //Services configuration 
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            //Repositories configuration 
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserManagerService, UserManagerService>();
            services.AddScoped<IRoleManagerService, RoleManagerService>();

            services.AddTransient<ICacheManager, MemoryCacheManager>();

            services.AddScoped<IBookManager, BookManager>();
            services.AddScoped<ICommentManager, CommentManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IBasketManager, BasketManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IRoleManager, RoleManager>();
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
