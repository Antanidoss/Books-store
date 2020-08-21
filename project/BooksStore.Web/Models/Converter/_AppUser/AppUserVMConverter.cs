using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Web.Models.ViewModels.AppUser;
using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Web.Converter._AppUser
{
    public static class AppUserVMConverter
    {
        public static AppUserViewModel ConvertToAppUserViewModel(AppUserDTO appUserDTO)
        {
            if(appUserDTO != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppUserDTO, AppUserViewModel>()).CreateMapper();
                return mapper.Map<AppUserDTO, AppUserViewModel>(appUserDTO);
            }

            return new AppUserViewModel();
        }

        public static IEnumerable<AppUserViewModel> ConvertToAppUserViewModel(IEnumerable<AppUserDTO> appUsersDTO)
        {
            if(appUsersDTO != null && appUsersDTO.Count() != 0)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppUserDTO, AppUserViewModel>()).CreateMapper();
                return mapper.Map<IEnumerable<AppUserDTO>, IEnumerable<AppUserViewModel>>(appUsersDTO);
            }

            return new List<AppUserViewModel>();
        }
    }
}
