using BooksStore.Core.AppUserModel;
using BooksStore.Web.Interface.Converter;
using BooksStore.Web.Models.ViewModels.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Converter._AppUser
{
    public class AppUserConverter : IAppUserConverter
    {

        public AppUserViewModel ConvertToAppUserViewModel(AppUser appUser)
        {
            if(appUser != null)
            {
                AppUserViewModel appUserViewModel = new AppUserViewModel()
                {
                    Id = appUser.Id,
                    Name = appUser.UserName,
                    Email = appUser.Email,
                    RoleName = string.Empty
                };
                return appUserViewModel;
            }
            return new AppUserViewModel();
        }

        public IEnumerable<AppUserViewModel> ConvertToAppUserViewModel(IEnumerable<AppUser> appUsers)
        {
            if(appUsers != null && appUsers.Count() != 0)
            {
                List<AppUserViewModel> appUserViewModels = new List<AppUserViewModel>();
                foreach(var p in appUsers)
                {
                    appUserViewModels.Add(ConvertToAppUserViewModel(p));
                }
                return appUserViewModels;
            }
            return new List<AppUserViewModel>();
        }
    }
}
