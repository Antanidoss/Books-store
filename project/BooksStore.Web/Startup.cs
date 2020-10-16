using AutoMapper;
using BooksStore.Core.AppUserModel;
using BooksStore.Infastructure;
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
using BooksStore.Service;
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

            //Infastructure layer configuration 
            services.AddInfastructure(Configuration);
            //Service layer configuration 
            services.AddService();

            //Antiforgery configuration 
            services.AddAntiforgery();

            // Authentication and Authorization configuration 
            services.AddAuthentication();
            services.AddAuthorization();

            // Auto mapper configuration
            var mappingConfig = new MapperConfiguration(mc =>
            {               
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

            //Current user configuration 
            services.AddScoped<ICurrentUser, CurrentUser>();

            //ViewModelServices configuration
            services.AddScoped<IBookViewModelService, BookViewModelService>();
            services.AddScoped<ICommentViewModelService, CommentViewModelService>();
            services.AddScoped<IOrderViewModelService, OrderViewModelService>();
            services.AddScoped<IBasketViewModelService, BasketViewModelService>();
            services.AddScoped<ICategoryViewModelService, CategoryViewModelService>();
            services.AddScoped<IRoleViewModelService, RoleViewModelService>();
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
