using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Interfaces.Managers
{
    public interface IOrderViewModelService
    {
        Task AddOrderAsync(OrderCreateModel model);
        Task<OrderViewModel> GetOrderByIdAsync(int orderId);
        Task RemoveOrderAsync(int orderId);
        Task<IEnumerable<OrderViewModel>> GetOrdersAsync(int pageNum);
        Task RemoveCompleteOrderAsync();
    }
}
