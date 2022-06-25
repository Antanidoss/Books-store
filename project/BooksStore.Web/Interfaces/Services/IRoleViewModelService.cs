using BooksStore.Common;
using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Web.Interfaces.Services
{
    public interface IRoleViewModelService
    {
        Task<Result> CreateRoleAsync(RoleCreateModel model);
        Task<IEnumerable<RoleViewModel>> GetRolesAsync(int pageNum);
        Task<RoleViewModel> FindRoleByIdAsync(string roleId);
        Task<Result> DeleteAsync(string roleId);
    }
}
