using AutoMapper;
using BooksStore.Core.BookModel;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Service.Profiles
{
    public class BookDTOProfile : Profile
    {
        public BookDTOProfile()
        {
            CreateMap<Book, BookDTO>();

            CreateMap<BookDTO, Book>()
                .ForMember(p => p.BookOrders, conf => conf.Ignore())
                .ForMember(p => p.BookBaskets, conf => conf.Ignore())
                .ForMember(p => p.Comments, conf => conf.Ignore());
        }
    }
}
