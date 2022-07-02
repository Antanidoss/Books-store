using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task RemoveAsync(Category category);
        Task UpdateAsync(Category category);
        Task<IEnumerable<Category>> GetAsync(int skip, int take, IQueryableFilterSpec<Category> filter);
        Task<IEnumerable<Category>> GetAsync(int skip, int take);
        Task<Category> GetAsync(IQueryableFilterSpec<Category> filter);
        Task<int> GetCountAsync();
    }
}