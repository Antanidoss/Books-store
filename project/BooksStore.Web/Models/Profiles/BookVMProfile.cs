using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Web.Models.ViewModels.Book;
using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Web.Profiles
{
    public class BookVMProfile : Profile
    {
        public BookVMProfile()
        {
            CreateMap<BookDTO, BookViewModel>()
                    .ForMember(p => p.AuthorFullName, conf => conf.MapFrom(o => o.AuthorFirstname + " " + o.AuthorSurname));            
        }
    }
}
