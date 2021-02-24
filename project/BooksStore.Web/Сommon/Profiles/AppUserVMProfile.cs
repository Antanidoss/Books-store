using AutoMapper;
using BooksStore.Services.DTO;
using BooksStore.Web.Сommon.ViewModel.ReadModel;

namespace BooksStore.Web.Сommon.Profiles
{
    public class AppUserVMProfile : Profile
    {
        public AppUserVMProfile()
        {
            CreateMap<AppUserDTO, AppUserViewModel>();
        }
    }
}
