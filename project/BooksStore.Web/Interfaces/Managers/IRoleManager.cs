using BooksStore.Infrastructure;
using BooksStore.Service.DTO;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Interfaces.Managers
{
    public interface IRoleManager
    {
        Task<Result> CreateRoleAsync(RoleCreateModel model);
        Task<IEnumerable<RoleViewModel>> GetRolesAsync(int pageNum);
        Task<RoleViewModel> FindRoleByIdAsync(string roleId);
        Task<Result> DeleteAsync(RoleViewModel roleVM);
    }
}
