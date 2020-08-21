using AutoMapper;
using BooksStore.Core.BookModel;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Service.Converter
{
    internal static class BookDTOConverter
    {
        public static BookDTO ConvertToBookDTO(Book book)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();

            return map.Map<Book, BookDTO>(book);
        }
        public static IEnumerable<BookDTO> ConvertToBookDTO(IEnumerable<Book> books)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();

            return map.Map<IEnumerable<Book>, IEnumerable<BookDTO>>(books);
        }
        public static Book ConvertToBook(BookDTO bookDTO)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()
                .ForMember(p => p.BookOrders, conf => conf.Ignore())
                .ForMember(p => p.BookBaskets, conf => conf.Ignore())
                .ForMember(p => p.Comments, conf => conf.Ignore()))
                .CreateMapper();

            return map.Map<BookDTO, Book>(bookDTO);
        }
        public static IEnumerable<Book> ConvertToBook(IEnumerable<BookDTO> booksDTO)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()
                .ForMember(p => p.BookOrders, conf => conf.Ignore())
                .ForMember(p => p.BookBaskets, conf => conf.Ignore())
                .ForMember(p => p.Comments , conf => conf.Ignore()))
                .CreateMapper();

            return map.Map<IEnumerable<BookDTO>, IEnumerable<Book>>(booksDTO);
        }
    }
}
