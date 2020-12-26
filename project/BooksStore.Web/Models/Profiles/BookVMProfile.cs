using AutoMapper;
using BooksStore.Services.DTO;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;

namespace BooksStore.Web.Profiles
{
    public class BookVMProfile : Profile
    {
        public BookVMProfile()
        {
            CreateMap<BookDTO, BookViewModel>()
                    .ForMember(p => p.AuthorFullName, conf => conf.MapFrom(o => o.AuthorFirstname + " " + o.AuthorSurname));

            CreateMap<BookCreateModel, BookDTO>();
            CreateMap<BookUpdateModel, BookDTO>();
        }
    }
}
