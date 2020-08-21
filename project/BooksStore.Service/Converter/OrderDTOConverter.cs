using AutoMapper;
using BooksStore.Core.BookOrderJunctionModel;
using BooksStore.Core.OrderModel;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksStore.Service.Converter
{
    internal static class OrderDTOConverter
    {
        public static OrderDTO ConvertToOrderDTO(Order order)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()
            .ForMember(p => p.BooksOrder, conf => conf.MapFrom(o => 
            BookDTOConverter.ConvertToBookDTO(o.BookOrders.Select(f => f.Book))))).CreateMapper();

            return map.Map<Order, OrderDTO>(order);
        }
        public static IEnumerable<OrderDTO> ConvertToOrderDTO(IEnumerable<Order> orders)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()
            .ForMember(p => p.BooksOrder, conf => conf.MapFrom(o => 
            BookDTOConverter.ConvertToBookDTO(o.BookOrders.Select(f => f.Book))))).CreateMapper();

            return map.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orders);
        }
        public static Order ConvertToOrder(OrderDTO orderDTO)
        {
            List<BookOrderJunction> bookOrders = new List<BookOrderJunction>();
            foreach(var book in orderDTO.BooksOrder)
            {
                bookOrders.Add(new BookOrderJunction()
                {
                    BookId = book.Id,
                    OrderId = orderDTO.Id
                });
            }

            var map = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, Order>()
            .ForMember(p => p.AppUser, conf => conf.Ignore())
            .ForMember(p => p.UpdateTime, conf => conf.Ignore())
            .ForMember(p => p.BookOrders, conf => conf.MapFrom(p => bookOrders)))
            .CreateMapper();

            return map.Map<OrderDTO, Order>(orderDTO);
        }
    }
}
