using AutoMapper;
using BooksStore.Infrastructure;
using BooksStore.Infrastructure.Exceptions;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Service.Implementation.Identity
{
    public class RoleService : IRoleService
    {
        RoleManager<IdentityRole> RoleManager { get; set; }
        IMapper Mapper { get; set; }
        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            RoleManager = roleManager;
            Mapper = mapper;
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
                var result = await RoleManager.DeleteAsync(Mapper.Map<IdentityRole>(roleDTO));
                return result.ToApplicationResult();
            }
            return Result.Failure(new string[] { "Некорректные входные данные" });
        }

        public async Task<RoleDTO> FindRoleByIdAsync(string roleId)
        {           
            var role = Mapper.Map<RoleDTO>(await RoleManager.FindByIdAsync(roleId));

            if (role == null)
            {
                throw new NotFoundException(nameof(role), roleId);
            }

            return (role);                    
        }

        public async Task<IEnumerable<RoleDTO>> GetRolesAsync(int skip, int take)
        {
            return Mapper.Map<IEnumerable<RoleDTO>>(await RoleManager.Roles.Skip(skip).Take(take).ToListAsync());
        }
    }
}
