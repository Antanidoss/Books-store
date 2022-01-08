using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetAsync(int skip, int take, Expression<Func<Book, bool>> predicate);
        Task<IEnumerable<Book>> GetAsync(int skip, int take);
        Task<int> GetCountAsync();
    }
}