using AutoMapper;
using BooksStore.Services.DTO;
using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using BooksStore.Web.Сommon.ViewModel.UpdateModel;

namespace BooksStore.Web.Сommon.Profiles
{
    public class BookVMProfile : Profile
    {
        public BookVMProfile()
        {
            CreateMap<BookDTO, BookViewModel>()
                    .ForMember(p => p.AuthorFullName, conf => conf.MapFrom(o => o.AuthorFirstname + " " + o.AuthorSurname));

            CreateMap<BookCreateModel, BookDTO>();
            CreateMap<BookUpdateModel, BookDTO>();
            CreateMap<BookViewModel, BookUpdateModel>();
        }
    }
}
