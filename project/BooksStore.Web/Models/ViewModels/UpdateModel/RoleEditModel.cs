using BooksStore.Web.Models.ViewModels;
using System.Collections.Generic;

namespace BooksStore.Web.Models.UpdateModel
{
    public class RoleEditModel
    {
        public RoleViewModel RoleViewModel { get; set; }
        public IEnumerable<AppUserViewModel> Memebers { get; set; }
        public IEnumerable<AppUserViewModel> NonMembers { get; set; }
    }
}
