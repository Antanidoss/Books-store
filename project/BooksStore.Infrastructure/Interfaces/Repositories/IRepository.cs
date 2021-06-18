using BooksStore.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Infrastructure.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
