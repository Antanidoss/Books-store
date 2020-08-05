using BooksStore.Core.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Service.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task RemoveOrderAsync(int orderId);
        Task<IEnumerable<Order>> GetOrders(int skip, int take);
        Task UpdateOrderAsync(Order order);
        Task RemoveCompleteOrder(string userId);
        Task<IEnumerable<Order>> GetOrdersByAppUserId(string appUserId);
        Task<int> GetCountOrders();
    }
}
