using BooksStore.Web.Models.ViewModels.AppUser;
using BooksStore.Web.Models.ViewModels.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.UpdateModel.Role
{
    public class RoleEditModel
    {
        public RoleViewModel RoleViewModel { get; set; }
        public IEnumerable<AppUserViewModel> Memebers { get; set; }
        public IEnumerable<AppUserViewModel> NonMembers { get; set; }
    }
}
