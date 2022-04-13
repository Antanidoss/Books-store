using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BooksStore.AppConfigure.Models
{
    public class BaseConfigureModel
    {
        public readonly IServiceCollection Services;

        public readonly IConfiguration Configuration;

        public readonly IMapperConfigurationExpression MapperConfigure;

        public BaseConfigureModel(IServiceCollection services, IConfiguration configuration, IMapperConfigurationExpression mapperConfigure)
        {
            Services = services;
            Configuration = configuration;
            MapperConfigure = mapperConfigure;
        }
    }
}
