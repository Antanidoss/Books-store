using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Web.Models.ViewModel.ReadModel;

namespace BooksStore.Web.Profiles
{
    public class RoleVMProfile : Profile
    {
        public RoleVMProfile()
        {
            CreateMap<RoleDTO, RoleViewModel>();
        }
    }
}
