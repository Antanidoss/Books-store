using AutoMapper;
using BooksStore.Infrastructure;
using BooksStore.Infrastructure.Exceptions;
using BooksStore.Services.DTO;
using BooksStore.Services.Interfaces.IdentityServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Services.Implementation.IdentityServices
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
            var result = await _roleManager.CreateAsync(new IdentityRole() { Name = roleName });

            return result.ToApplicationResult();
        }

        public async Task<Result> DeleteAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var result = await _roleManager.DeleteAsync(role);

            return result.ToApplicationResult();
        }

        public async Task<RoleDTO> FindRoleByIdAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                throw new NotFoundException(nameof(role), roleId);
            }

            return (_mapper.Map<RoleDTO>(role));
        }

        public async Task<IEnumerable<RoleDTO>> GetRolesAsync(int skip, int take)
        {
            var roles = await _roleManager.Roles.Skip(skip).Take(take).ToListAsync();

            return _mapper.Map<IEnumerable<RoleDTO>>(roles);
        }
    }
}