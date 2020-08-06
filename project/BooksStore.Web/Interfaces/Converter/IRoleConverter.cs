using BooksStore.Web.Models.ViewModels.Role;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Interface
{
    public interface IRoleConverter
    {
        RoleViewModel ConvertToRoleViewModel(IdentityRole identityRole);
        IEnumerable<RoleViewModel> ConvertToRoleViewModel(IEnumerable<IdentityRole> identityRoles);
    }
}
