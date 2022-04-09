using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Services.DTO.Basket;
using BooksStore.Services.DTO.Book;
using System.Linq;

namespace BooksStore.Services.Profiles
{
    internal sealed class BasketDTOProfile : Profile
    {
        public BasketDTOProfile()
        {
            CreateMap<BookDTO, BookBasketJunction>()
                .ForMember(p => p.BookId, conf => conf.MapFrom(o => o.Id))
                .ForMember(p => p.Id, conf => conf.Ignore());

            CreateMap<Basket, BasketDTO>()
                .ForMember(p => p.BasketBooks, conf => conf.MapFrom(o => o.BasketBooks.Select(f => f.Book)));

            CreateMap<BasketDTO, Basket>()
                .ForMember(p => p.BasketBooks, conf => conf.MapFrom(o => o.BasketBooks));
        }
    }
}
