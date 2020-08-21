using AutoMapper;
using BooksStore.Core.BasketModel;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksStore.Service.Converter
{
    internal static class BasketDTOConverter
    {
        public static BasketDTO ConvertToBasketDTO(Basket basket)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<Basket, BasketDTO>()
            .ForMember(p => p.BasketBooks, conf => conf.MapFrom(o => 
            BookDTOConverter.ConvertToBookDTO(o.BookBaskets.Select(f => f.Book))))).CreateMapper();

            return map.Map<Basket, BasketDTO>(basket);
        }
        public static IEnumerable<BasketDTO> ConvertToBasketDTO(IEnumerable<Basket> baskets)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<Basket, BasketDTO>()
            .ForMember(p => p.BasketBooks, conf => conf.MapFrom(o => 
            BookDTOConverter.ConvertToBookDTO(o.BookBaskets.Select(f => f.Book))))).CreateMapper();

            return map.Map<IEnumerable<Basket>, IEnumerable<BasketDTO>>(baskets);
        }
        public static Basket ConvertToBasket(BasketDTO basketDTO)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<BasketDTO, Basket>()
            .ForMember(p => p.BookBaskets.Select(p => p.Book), conf => conf.MapFrom(o => o.BasketBooks))).CreateMapper();

            return map.Map<BasketDTO, Basket>(basketDTO);
        }
    }
}
