using AutoMapper;
using BooksStore.Infrastructure;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces.Identity;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.Managers
{
    public class RoleManager : IRoleManager
    {
        private readonly IRoleManagerService _roleManagerService;

        private readonly IMapper _mapper;
        
        public RoleManager(IRoleManagerService roleManagerService, IMapper mapper)
        {
            _roleManagerService = roleManagerService;
            _mapper = mapper;
        }

        public async Task<Result> CreateRoleAsync(RoleCreateModel model)
        {            
            return await _roleManagerService.CreateRoleAsync(model.Name);
        }

        public async Task<Result> DeleteAsync(RoleViewModel roleVM)
        {
            return await _roleManagerService.DeleteAsync(_mapper.Map<RoleDTO>(roleVM));
        }

        public async Task<RoleViewModel> FindRoleByIdAsync(string roleId)
        {
            return _mapper.Map<RoleViewModel>(await _roleManagerService.FindRoleByIdAsync(roleId));
        }

        public async Task<IEnumerable<RoleViewModel>> GetRolesAsync(int pageNum)
        {
            int pageSize = PageSizes.Roles;

            var roles = await _roleManagerService.GetRolesAsync((pageNum - 1) * pageNum, pageSize);

            return _mapper.Map<IEnumerable<RoleViewModel>>(roles);
        }
    }
}
