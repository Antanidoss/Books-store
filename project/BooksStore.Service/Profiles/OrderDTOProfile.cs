using AutoMapper;
using BooksStore.Core.BookOrderJunctionModel;
using BooksStore.Core.OrderModel;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksStore.Service.Profiles
{
    public class OrderDTOProfile : Profile
    {
        public OrderDTOProfile()
        {
            CreateMap<BookDTO, BookOrderJunction>()
                .ForMember(p => p.BookId, conf => conf.MapFrom(o => o.Id))
                .ForMember(p => p.Id, conf => conf.Ignore());

            CreateMap<Order, OrderDTO>()
                .ForMember(p => p.BooksOrder, conf => conf.MapFrom(o => o.OrderBooks.Select(f => f.Book)));

            CreateMap<OrderDTO, Order>()
                .ForMember(p => p.AppUser, conf => conf.Ignore())
                .ForMember(p => p.UpdateTime, conf => conf.Ignore())
                .ForMember(p => p.OrderBooks, conf => conf.MapFrom(p => p.BooksOrder));
        }
        
    }
}
