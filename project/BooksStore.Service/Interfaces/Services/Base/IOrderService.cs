using BooksStore.Services.DTO.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.Interfaces.Services.Base
{
    public interface IOrderService
    {
        Task AddOrderAsync(IEnumerable<int> booksId, string userId);
        Task RemoveOrderAsync(int orderId);
        Task<IEnumerable<OrderDTO>> GetOrders(string appUserId, int skip, int take);
        Task RemoveCompleteOrder(string userId);
        Task<int> GetCountOrders(string appUserId);
    }
}
