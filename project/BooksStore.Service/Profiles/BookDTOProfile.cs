using AutoMapper;
using BooksStore.Common.Helpers;
using BooksStore.Core.Entities;
using BooksStore.Services.DTO.Book;
using Microsoft.AspNetCore.Http;

namespace BooksStore.Services.Profiles
{
    internal sealed class BookDTOProfile : Profile
    {
        public BookDTOProfile()
        {
            CreateMap<Book, BookDTO>()
                .ForMember(p => p.ImageData, conf => conf.MapFrom(o => o.Img.ImageData))
                .ForMember(p => p.ImageName, conf => conf.MapFrom(o => o.Img.Name));

            CreateMap<BookDTO, Book>()
                .ForMember(p => p.Author, conf => conf.MapFrom(o => new Author(o.AuthorFirstname, o.AuthorSurname) { Id = o.AuthorId }))
                .ForMember(p => p.Img, conf => conf.MapFrom(o => new Img(o.ImageName) {Id = o.ImgId, BookId = o.Id, ImageData = o.ImageData}))
                .ForMember(p => p.BookOrders, conf => conf.Ignore())
                .ForMember(p => p.BookBaskets, conf => conf.Ignore())
                .ForMember(p => p.Comments, conf => conf.Ignore());

            CreateMap<BookDTOCreateModel, Book>()
                .ForMember(d => d.Img, conf => conf.MapFrom(o => new Img(o.ImgFile.FileName) {ImageData = GetImageData(o.ImgFile)}));
        }

        private byte[] GetImageData(IFormFile image)
        {
            using var stream = image.OpenReadStream();

            return ImageBookHelper.GetImageData(stream);
        }
    }
}
