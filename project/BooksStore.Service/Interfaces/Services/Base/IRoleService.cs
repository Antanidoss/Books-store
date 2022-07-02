using BooksStore.Common;
using BooksStore.Services;
using BooksStore.Services.DTO.Role;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.Interfaces.Services.Base
{
    public interface IRoleService
    {
        Task<Result> CreateRoleAsync(string roleName);
        Task<IEnumerable<RoleDTO>> GetRolesAsync(int skip, int take);
        Task<RoleDTO> FindRoleByIdAsync(string roleId);
        Task<Result> DeleteAsync(string roleId);
    }
}