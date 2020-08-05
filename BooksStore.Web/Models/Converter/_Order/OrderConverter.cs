using BooksStore.Core.BookModel;
using BooksStore.Core.OrderModel;
using BooksStore.Web.Interface.Converter;
using BooksStore.Web.Models.ViewModels.Book;
using BooksStore.Web.Models.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Converter._Order
{
    public class OrderConverter : IOrderConverter
    {
        IBookConverter BookConverter { get; set; }
        public OrderConverter(IBookConverter bookConverter) => BookConverter = bookConverter;
        public OrderViewModel ConvertToOrderViewModel(Order order)
        {
            if (order != null)
            {
                OrderViewModel orderViewModel = new OrderViewModel()
                {
                    Id = order.Id,
                    TimeCreate = order.TimeCreate,
                    TimeOfDelivery = order.TimeOfDelivery,
                    BooksViewModel = BookConverter.ConvertToBookViewModel(order.BookOrders.Select(p => p.Book)).ToList()
                };

                return orderViewModel;
            }
            return new OrderViewModel();
        }

        public IEnumerable<OrderViewModel> ConvertToOrderViewModel(IEnumerable<Order> orders)
        {
            if (orders != null && orders.Count() != 0)
            {
                List<OrderViewModel> ordersViewModel = new List<OrderViewModel>();
                foreach (var order in orders)
                {
                    ordersViewModel.Add(ConvertToOrderViewModel(order));
                }
                return ordersViewModel;
            }
            return new List<OrderViewModel>();
        }
    }
}
