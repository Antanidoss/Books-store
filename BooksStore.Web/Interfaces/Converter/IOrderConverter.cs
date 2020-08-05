using BooksStore.Core.BookModel;
using BooksStore.Core.OrderModel;
using BooksStore.Web.Models.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Interface.Converter
{
    public interface IOrderConverter
    {
        OrderViewModel ConvertToOrderViewModel(Order order);
        IEnumerable<OrderViewModel> ConvertToOrderViewModel(IEnumerable<Order> orders);
    }
}
