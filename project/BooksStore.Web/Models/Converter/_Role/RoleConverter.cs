using BooksStore.Web.Interface;
using BooksStore.Web.Interface.Converter;
using BooksStore.Web.Models.ViewModels.Role;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Converter._Role
{
    public class RoleConverter : IRoleConverter
    {
        public IAppUserConverter AppUserConverter { get; set; }
        public RoleConverter(IAppUserConverter appUserConverter) => AppUserConverter = appUserConverter;
        public RoleViewModel ConvertToRoleViewModel(IdentityRole identityRole)
        {
            if(identityRole != null)
            {
                RoleViewModel roleViewModel = new RoleViewModel()
                {
                    Id = identityRole.Id,
                    Name = identityRole.Name
                };
                return roleViewModel;
            }
            return new RoleViewModel();
        }

        public IEnumerable<RoleViewModel> ConvertToRoleViewModel(IEnumerable<IdentityRole> identityRoles)
        {
            if(identityRoles != null && identityRoles.Count() != 0)
            {
                List<RoleViewModel> rolesViewModel = new List<RoleViewModel>();
                foreach(var role in identityRoles)
                {
                    rolesViewModel.Add(ConvertToRoleViewModel(role));
                }
                return rolesViewModel;
            }
            return new List<RoleViewModel>();
        }
    }
}
