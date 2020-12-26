using BooksStore.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task<Order> GetOrderById(int id);
        Task RemoveOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrders(int skip, int take);
        Task UpdateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersByAppUserId(string appUserId);
        Task<int> GetCountOrders();
    }
}
