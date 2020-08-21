using BooksStore.Infrastructure;
using BooksStore.Service.Converter;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Service.Implementation.Identity
{
    public class RoleManagerService : IRoleManagerService
    {
        RoleManager<IdentityRole> RoleManager { get; set; }
        public RoleManagerService(RoleManager<IdentityRole> roleManager)
        {
            RoleManager = roleManager;
        }

        public async Task<Result> CreateRoleAsync(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var result = await RoleManager.CreateAsync(new IdentityRole() { Name = roleName });
                return result.ToApplicationResult();
            }
            return Result.Failure(new string[] { "Некорректные входные данные" });
        }

        public async Task<Result> DeleteAsync(RoleDTO roleDTO)
        {
            if(roleDTO != null && roleDTO != default)
            {
                var result = await RoleManager.DeleteAsync(RoleDTOConverter.ConvertToRole(roleDTO));
                return result.ToApplicationResult();
            }
            return Result.Failure(new string[] { "Некорректные входные данные" });
        }

        public async Task<(Result Result, RoleDTO RoleDTO)> FindRoleByIdAsync(string roleId)
        {
            if (!string.IsNullOrEmpty(roleId))
            {
                var role = RoleDTOConverter.ConvertToRoleDTO(await RoleManager.FindByIdAsync(roleId));
                if(role != null)
                {
                    return (Result.Success(), role);
                }

                return (IdentityResultExtensions.RoleNotFound(), new RoleDTO());
            }
            return (Result.Failure(new string[] { "Некорректные входные данные" }) , new RoleDTO());
        }

        public async Task<IEnumerable<RoleDTO>> GetRolesAsync()
        {
            return RoleDTOConverter.ConvertToRoleDTO(await RoleManager.Roles.ToListAsync());
        }
    }
}
