using AutoMapper;
using BooksStore.Infrastructure;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces.Identity;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.Managers
{
    public class RoleViewModelService : IRoleViewModelService
    {
        private readonly IRoleService _roleManagerService;

        private readonly IUserService _userService;

        private readonly IMapper _mapper;
        
        public RoleViewModelService(IRoleService roleManagerService, IMapper mapper)
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

        public async Task<Result> UpdateAsync(RoleUpdateModel model)
        {
            foreach (var userId in model.IdsToAdd)
            {
                var result = await _userService.FindAppUserByIdAsync(userId);
                if (!result.Result.Succeeded)
                {
                    return result.Result;
                }
                await _userService.AddToRoleAsync(result.AppUserDTO, model.Name);
            }

            foreach (var userId in model.IdsToDelete)
            {
                var user = (await _userService.FindAppUserByIdAsync(userId)).AppUserDTO;
                if (user != null)
                {
                    await _userService.RemoveFromRoleAsync(user, model.Name);
                }
            }

            return Result.Success();
        }
    }
}
