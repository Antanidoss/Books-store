using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Web.Models.ViewModels.Role;
using System.Collections.Generic;
using System.Linq;

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
