using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task RemoveAsync(Order order);
        Task UpdateAsync(Order order);
        Task<IEnumerable<Order>> GetAsync(int skip, int take, IQueryableFilterSpec<Order> filter);
        Task<Order> GetAsync(IQueryableFilterSpec<Order> filter);
        Task<int> GetCountAsync(IQueryableFilterSpec<Order> filter);
    }
}