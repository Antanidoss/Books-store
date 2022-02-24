using AutoMapper;
using BooksStore.Infastructure;
using BooksStore.Infastructure.Data;
using BooksStore.Services;
using BooksStore.Web.Common.CurUser;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Interfaces.Services;
using BooksStore.Web.Сommon.Profiles;
using BooksStore.Web.Сommon.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
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

            var mapperConfigureExpression = new AutoMapper.Configuration.MapperConfigurationExpression();

            //Service layer configuration 
            services.AddService(mapperConfigureExpression);

            //Antiforgery configuration 
            services.AddAntiforgery();

            // Authentication and Authorization configuration 
            services.AddAuthentication();
            services.AddAuthorization();

            // Auto mapper configuration
            mapperConfigureExpression.AddProfile(new AppUserVMProfile());
            mapperConfigureExpression.AddProfile(new BookVMProfile());
            mapperConfigureExpression.AddProfile(new CommentVMProfile());
            mapperConfigureExpression.AddProfile(new CategoryVMProfile());
            mapperConfigureExpression.AddProfile(new RoleVMProfile());
            mapperConfigureExpression.AddProfile(new OrderVMProfile());

            var mappingConfig = new MapperConfiguration(mapperConfigureExpression);

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
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<EFDbContext>();
                if ((context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                {
                    context.Database.EnsureCreated();
                }
            }

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