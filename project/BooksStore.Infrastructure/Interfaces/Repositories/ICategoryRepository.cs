using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task<IEnumerable<Category>> GetAsync(int skip, int take);
        public Task<Category> GetByNameAsync(string categoryName);
        Task<int> GetCount();
    }
}
