using AutoMapper;
using BooksStore.AppConfigure;
using BooksStore.AppConfigure.Models;
using BooksStore.Web.Common.CurUser;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Interfaces.Services;
using BooksStore.Web.Сommon.Profiles;
using BooksStore.Web.Сommon.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

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

            //Antiforgery configuration 
            services.AddAntiforgery();

            // Authentication and Authorization configuration 
            services.AddAuthentication();
            services.AddAuthorization();

            var mapperConfigureExpression = new AutoMapper.Configuration.MapperConfigurationExpression();
            AppConfigureManager.RegisterDependencies(new BaseConfigureModel(services, Configuration, mapperConfigureExpression));
            AddViewMapperProfiles(mapperConfigureExpression);

            services.AddSingleton(new MapperConfiguration(mapperConfigureExpression).CreateMapper());

            //Current user configuration 
            services.AddScoped<ICurrentUser, CurrentUser>();

            AddViewServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Task.Run(async () => await AppConfigureManager.CreateDBIfNotExistAsync(app));

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

        private void AddViewMapperProfiles(AutoMapper.Configuration.MapperConfigurationExpression mapperConfigurationExpression)
        {
            mapperConfigurationExpression.AddProfile(new AppUserVMProfile());
            mapperConfigurationExpression.AddProfile(new BookVMProfile());
            mapperConfigurationExpression.AddProfile(new CommentVMProfile());
            mapperConfigurationExpression.AddProfile(new CategoryVMProfile());
            mapperConfigurationExpression.AddProfile(new RoleVMProfile());
            mapperConfigurationExpression.AddProfile(new OrderVMProfile());
        }

        private void AddViewServices(IServiceCollection services)
        {
            services.AddScoped<IBookViewModelService, BookViewService>();
            services.AddScoped<ICommentViewModelService, CommentViewService>();
            services.AddScoped<IOrderViewModelService, OrderViewService>();
            services.AddScoped<IBasketViewModelService, BasketViewService>();
            services.AddScoped<ICategoryViewModelService, CategoryViewService>();
            services.AddScoped<IRoleViewModelService, RoleViewService>();
        }
    }
}