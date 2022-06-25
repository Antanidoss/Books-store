using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task AddAsync(Book book);
        Task RemoveAsync(Book book);
        Task UpdateAsync(Book book);
        Task<IEnumerable<Book>> GetAsync(int skip, int take, IQueryableFilterSpec<Book> filter);
        Task<IEnumerable<Book>> GetAsync(int skip, int take);
        Task<Book> GetAsync(IQueryableFilterSpec<Book> filter);
        Task<int> GetCountAsync();
    }
}