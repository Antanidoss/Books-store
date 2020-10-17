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
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IMapper _mapper;
        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<Result> CreateRoleAsync(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole() { Name = roleName });
                return result.ToApplicationResult();
            }
            return Result.Failure(new string[] { "Некорректные входные данные" });
        }

        public async Task<Result> DeleteAsync(RoleDTO roleDTO)
        {
            if(roleDTO != null && roleDTO != default)
            {
                var result = await _roleManager.DeleteAsync(_mapper.Map<IdentityRole>(roleDTO));
                return result.ToApplicationResult();
            }
            return Result.Failure(new string[] { "Некорректные входные данные" });
        }

        public async Task<RoleDTO> FindRoleByIdAsync(string roleId)
        {           
            var role = _mapper.Map<RoleDTO>(await _roleManager.FindByIdAsync(roleId));

            if (role == null)
            {
                throw new NotFoundException(nameof(role), roleId);
            }

            return (role);                    
        }

        public async Task<IEnumerable<RoleDTO>> GetRolesAsync(int skip, int take)
        {
            return _mapper.Map<IEnumerable<RoleDTO>>(await _roleManager.Roles.Skip(skip).Take(take).ToListAsync());
        }
    }
}
