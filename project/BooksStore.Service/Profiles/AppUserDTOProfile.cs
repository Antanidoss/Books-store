using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Services.DTO;

namespace BooksStore.Services.Profiles
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
