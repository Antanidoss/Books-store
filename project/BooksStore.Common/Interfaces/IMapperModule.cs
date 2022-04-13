using AutoMapper;

namespace BooksStore.Common.Interfaces
{
    public interface IMapperModule
    {
        void RegisterMapperProfiles(IMapperConfigurationExpression mapperConfigure);
    }
}
