using Microsoft.AspNetCore.Builder;

namespace BooksStore.Common.Interfaces
{
    public interface IDBConfigureModule
    {
        void ConfigureDB(IApplicationBuilder app);
    }
}
