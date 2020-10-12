using AutoMapper;
using BooksStore.Core.AuthorModel;
using BooksStore.Core.BookModel;
using BooksStore.Core.ImageModel;
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
                .ForMember(p => p.Author, conf => conf.MapFrom(o => new Author() { Firstname = o.AuthorFirstname, Surname = o.AuthorSurname, Id = o.AuthorId }))
                .ForMember(d => d.Img, conf => conf.MapFrom(o => new Img() { Path = o.ImgPath, Id = o.ImgId, BookId = o.Id }))
                .ForMember(p => p.BookOrders, conf => conf.Ignore())
                .ForMember(p => p.BookBaskets, conf => conf.Ignore())
                .ForMember(p => p.Comments, conf => conf.Ignore());
        }
    }
}
