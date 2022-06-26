using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Web.Interfaces.Services
{
    public interface IOrderViewModelService
    {
        Task AddOrderAsync(OrderCreateModel model);
        Task RemoveOrderAsync(int orderId);
        Task<IEnumerable<OrderViewModel>> GetOrdersAsync(int pageNum);
        Task RemoveCompleteOrderAsync();
    }
}
