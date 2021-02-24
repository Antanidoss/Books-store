using BooksStore.Web.Сommon.ViewModel.Index;
using System.Collections.Generic;

namespace BooksStore.Web.Сommon.ViewModel.ReadModel
{
    public class OrderListViewModel
    {
        public IndexViewModel<OrderViewModel> OrderIndexModel { get; set; }

        public OrderListViewModel(int pageNum, int pageSize, int totalItems, IEnumerable<OrderViewModel> orders)
        {
            OrderIndexModel = new IndexViewModel<OrderViewModel>(pageNum, pageSize, totalItems, orders);
        }
    }
}
