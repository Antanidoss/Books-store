using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Services.DTO.AppUser;

namespace BooksStore.Services.Profiles
{
    internal sealed class AppUserDTOProfile : Profile
    {     
        public AppUserDTOProfile()
        {
            CreateMap<AppUser, AppUserDTO>();
            CreateMap<AppUserDTO, AppUser>();
        }        
    }
}
