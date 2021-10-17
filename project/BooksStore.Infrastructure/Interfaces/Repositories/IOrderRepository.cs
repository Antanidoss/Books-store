using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetAsync(string appUserId, int skip, int take);
        Task<int> GetCountAsync(string appUserId);
    }
}