using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces.Repositories;
using QueryableFilterSpecification.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetByFilterAsync(int skip, int take, IQueryableFilterSpec<Book> filter);
        Task<IEnumerable<Book>> GetAsync(int skip, int take);
        Task<int> GetCountAsync();
    }
}