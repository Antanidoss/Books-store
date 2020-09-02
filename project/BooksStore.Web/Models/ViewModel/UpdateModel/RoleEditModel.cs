using BooksStore.Web.Models.ViewModel.ReadModel;
using System.Collections.Generic;

namespace BooksStore.Web.Models.ViewModel.UpdateModel
{
    public class RoleEditModel
    {
        public RoleViewModel RoleViewModel { get; set; }
        public IEnumerable<AppUserViewModel> Memebers { get; set; }
        public IEnumerable<AppUserViewModel> NonMembers { get; set; }
    }
}
