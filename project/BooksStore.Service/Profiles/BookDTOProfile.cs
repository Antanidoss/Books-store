using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Services.DTO.Book;

namespace BooksStore.Services.Profiles
{
    public class BookDTOProfile : Profile
    {
        public BookDTOProfile()
        {
            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>()
                .ForMember(p => p.Author, conf => conf.MapFrom(o => new Author(o.AuthorFirstname, o.AuthorSurname) { Id = o.AuthorId }))
                .ForMember(d => d.Img, conf => conf.MapFrom(o => new Img(o.ImgPath) { Id = o.ImgId, BookId = o.Id }))
                .ForMember(p => p.BookOrders, conf => conf.Ignore())
                .ForMember(p => p.BookBaskets, conf => conf.Ignore())
                .ForMember(p => p.Comments, conf => conf.Ignore());

            CreateMap<BookDTOCreateModel, Book>()
                .ForMember(d => d.Img, conf => conf.MapFrom(o => new Img(o.ImgPath)));
        }
    }
}
