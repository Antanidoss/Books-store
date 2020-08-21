using BooksStore.Core.OrderModel;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Service.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(OrderDTO orderDTO);
        Task<OrderDTO> GetOrderByIdAsync(int orderId);
        Task RemoveOrderAsync(int orderId);
        Task<IEnumerable<OrderDTO>> GetOrders(int skip, int take);
        Task UpdateOrderAsync(OrderDTO orderDTO);
        Task RemoveCompleteOrder(string userId);
        Task<IEnumerable<OrderDTO>> GetOrdersByAppUserId(string appUserId);
        Task<int> GetCountOrders();
    }
}
