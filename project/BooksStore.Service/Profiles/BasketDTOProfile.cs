using AutoMapper;
using BooksStore.Core.BasketModel;
using BooksStore.Core.BookBasketJunctionModel;
using BooksStore.Core.BookModel;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksStore.Service.Profiles
{
    public class BasketDTOProfile : Profile
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
