using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Services.DTO.Book;
using BooksStore.Services.DTO.Order;
using System.Linq;

namespace BooksStore.Services.Profiles
{
    public class OrderDTOProfile : Profile
    {
        public OrderDTOProfile()
        {
            CreateMap<BookDTO, BookOrderJunction>()
                .ForMember(p => p.BookId, conf => conf.MapFrom(o => o.Id))
                .ForMember(p => p.Id, conf => conf.Ignore());

            CreateMap<Order, OrderDTO>()
                .ForMember(p => p.OrderBooks, conf => conf.MapFrom(o => o.OrderBooks.Select(f => f.Book)));

            CreateMap<OrderDTO, Order>()
                .ForMember(p => p.AppUser, conf => conf.Ignore())
                .ForMember(p => p.UpdateTime, conf => conf.Ignore())
                .ForMember(p => p.OrderBooks, conf => conf.MapFrom(p => p.OrderBooks));

            CreateMap<int, BookOrderJunction>()
                .ForMember(o => o.BookId, conf => conf.MapFrom(i => i))
                .ForMember(o => o.Book, conf => conf.Ignore())
                .ForMember(o => o.Order, conf => conf.Ignore())
                .ForMember(o => o.OrderId, conf => conf.Ignore());
        }
    }
}