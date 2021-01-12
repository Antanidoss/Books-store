using BooksStore.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(int id);
        Task RemoveOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersAsync(string appUserId, int skip, int take);
        Task<int> GetCountOrdersAsync(string appUserId);
    }
}
