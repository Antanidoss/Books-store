using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        Task AddAsync(Author author);
        Task RemoveAsync(Author author);
        Task UpdateAsync(Author author);
        Task<IEnumerable<Author>> GetAsync(int skip, int take);
        Task<IEnumerable<Author>> GetAsync(int skip, int take, IQueryableFilterSpec<Author> filter);
        Task<Author> GetAsync(IQueryableFilterSpec<Author> filter);
        Task<int> GetCountAsync();
    }
}