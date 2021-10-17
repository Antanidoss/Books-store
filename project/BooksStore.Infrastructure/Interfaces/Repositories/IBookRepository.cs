using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetAsync(int skip, int take, Func<Book, bool> predicate);
        Task<IEnumerable<Book>> GetAsync(int skip, int take);
        Task<int> GetCountAsync();
    }
}