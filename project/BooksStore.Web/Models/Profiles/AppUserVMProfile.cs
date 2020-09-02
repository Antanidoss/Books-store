using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Web.Models.ViewModels.AppUser;
using System.Collections.Generic;
using System.Linq;

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
