using BooksStore.Core.AppUserModel;
using BooksStore.Web.Converter._AppUser;
using BooksStore.Web.Models.ViewModels.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Interface.Converter
{
    public interface IAppUserConverter
    {
        AppUserViewModel ConvertToAppUserViewModel(AppUser appUser);
        IEnumerable<AppUserViewModel> ConvertToAppUserViewModel(IEnumerable<AppUser> appUsers);
    }
}
