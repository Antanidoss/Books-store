using AutoMapper;
using BooksStore.Core.AppUserModel;
using BooksStore.Service.DTO;
using System.Collections.Generic;

namespace BooksStore.Service.Profiles
{
    public class AppUserDTOProfile : Profile
    {     
        public AppUserDTOProfile()
        {
            CreateMap<AppUser, AppUserDTO>();
            CreateMap<AppUserDTO, AppUser>();
        }        
    }
}
