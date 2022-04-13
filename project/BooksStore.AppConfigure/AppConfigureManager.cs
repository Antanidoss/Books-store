using BooksStore.AppConfigure.Models;
using BooksStore.Infastructure;
using BooksStore.Services;
using Microsoft.AspNetCore.Builder;

namespace BooksStore.AppConfigure
{
    public static class AppConfigureManager
    {
        public static void BaseConfigure(BaseConfigureModel configureModel)
        {
            ConfigureInfrastructure(configureModel);
            ConfigureServices(configureModel);
        }

        public static void CreateDBIfNotExist(IApplicationBuilder app)
        {
            new InfrastructureConfigureModule().ConfigureDB(app);
        }

        private static void ConfigureInfrastructure(BaseConfigureModel configureModel)
        {
            new InfrastructureConfigureModule().RegisterDependencies(configureModel.Services, configureModel.Configuration);
        }

        private static void ConfigureServices(BaseConfigureModel configureModel)
        {
            var servicesConfigureModule = new ServicesConfigureModule();

            servicesConfigureModule.RegisterDependencies(configureModel.Services, configureModel.Configuration);
            servicesConfigureModule.RegisterMapperProfiles(configureModel.MapperConfigure);
        }
    }
}
