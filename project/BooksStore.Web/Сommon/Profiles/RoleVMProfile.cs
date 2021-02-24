using AutoMapper;
using BooksStore.Services.DTO;
using BooksStore.Web.Сommon.ViewModel.ReadModel;

namespace BooksStore.Web.Сommon.Profiles
{
    public class RoleVMProfile : Profile
    {
        public RoleVMProfile()
        {
            CreateMap<RoleDTO, RoleViewModel>();
            CreateMap<RoleViewModel, RoleDTO>();
        }
    }
}
