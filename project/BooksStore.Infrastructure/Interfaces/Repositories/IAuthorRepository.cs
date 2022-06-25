using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces.Repositories;
using QueryableFilterSpecification.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetAsync(int skip, int take);
        Task<IEnumerable<Author>> GetAsync(int skip, int take, IQueryableFilterSpec<Author> filter);
        Task<Author> GetAsync(IQueryableFilterSpec<Author> filter);
        Task<int> GetCountAsync();
    }
}