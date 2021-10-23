using BooksStore.Services.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(IEnumerable<int> booksId, string userId);
        Task<OrderDTO> GetOrderByIdAsync(int orderId);
        Task RemoveOrderAsync(int orderId);
        Task<IEnumerable<OrderDTO>> GetOrders(string appUserId, int skip, int take);
        Task RemoveCompleteOrder(string userId);
        Task<int> GetCountOrders(string appUserId);
    }
}
