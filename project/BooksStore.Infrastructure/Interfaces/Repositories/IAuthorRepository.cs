using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetAsync(int skip, int take);
        Task<IEnumerable<Author>> GetAsync(int skip, int take, Expression<Func<Author, bool>> condition);
        Task<int> GetCountAsync();
    }
}