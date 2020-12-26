using BooksStore.Infrastructure;
using BooksStore.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Services.Interfaces.Identity
{
    public interface IRoleService
    {
        Task<Result> CreateRoleAsync(string roleName);
        Task<IEnumerable<RoleDTO>> GetRolesAsync(int skip, int take);
        Task<RoleDTO> FindRoleByIdAsync(string roleId);
        Task<Result> DeleteAsync(string roleId);
    }
}
