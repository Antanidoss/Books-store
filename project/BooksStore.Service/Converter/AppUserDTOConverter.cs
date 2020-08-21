using AutoMapper;
using BooksStore.Core.AppUserModel;
using BooksStore.Service.DTO;
using System.Collections.Generic;

namespace BooksStore.Service.Converter
{
    internal static class AppUserDTOConverter
    {     
        public static AppUser ConvertToAppUser(AppUserDTO appUserDTO)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<AppUserDTO, AppUser>()).CreateMapper();
            return map.Map<AppUserDTO, AppUser>(appUserDTO);
        }               

        public static AppUserDTO ConvertToAppUserDTO(AppUser appUser)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<AppUser, AppUserDTO>()).CreateMapper();
            return map.Map<AppUser, AppUserDTO>(appUser);
        }

        public static IEnumerable<AppUserDTO> ConvertToAppUserDTO(IEnumerable<AppUser> appUsers)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<AppUser, AppUserDTO>()).CreateMapper();
            return map.Map<IEnumerable<AppUser>, IEnumerable<AppUserDTO>>(appUsers);
        }
    }
}
