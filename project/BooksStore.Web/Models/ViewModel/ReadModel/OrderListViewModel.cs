using BooksStore.Web.Models.ViewModel.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.ViewModel.ReadModel
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
