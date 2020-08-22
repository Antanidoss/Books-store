using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Web.Models.ViewModels.Book;
using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Web.Converter._Book
{
    public static class BookVMConverter
    {
        public static BookViewModel ConvertToBookViewModel(BookDTO bookDTO)
        {
            if(bookDTO != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, BookViewModel>()
                    .ForMember(p => p.AuthorFullName, conf => conf.MapFrom(o => o.AuthorFirstname + " " + o.AuthorSurname)))
                    .CreateMapper();

                return mapper.Map<BookDTO, BookViewModel>(bookDTO);
            }
            return new BookViewModel();
        }

        public static IEnumerable<BookViewModel> ConvertToBookViewModel(IEnumerable<BookDTO> booksDTO)
        {
            if(booksDTO != null && booksDTO.Count() != 0)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, BookViewModel>()
                    .ForMember(p => p.AuthorFullName, conf => conf.MapFrom(o => o.AuthorFirstname + " " + o.AuthorSurname)))
                    .CreateMapper();

                return mapper.Map<IEnumerable<BookDTO>, IEnumerable<BookViewModel>>(booksDTO);
            }
            return new List<BookViewModel>();
        }
    }
}
