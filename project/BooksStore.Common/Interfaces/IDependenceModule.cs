using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace BooksStore.Common.Interfaces
{
    public interface IDependenceModule
    {
        IServiceCollection RegisterDependencies(IServiceCollection services, IConfiguration Configuration);
    }
}
