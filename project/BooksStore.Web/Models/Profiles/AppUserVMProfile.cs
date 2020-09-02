using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Web.Models.ViewModel.ReadModel;

namespace BooksStore.Web.Profiles
{
    public class AppUserVMProfile : Profile
    {
        public AppUserVMProfile()
        {
            CreateMap<AppUserDTO, AppUserViewModel>();
        }
    }
}
