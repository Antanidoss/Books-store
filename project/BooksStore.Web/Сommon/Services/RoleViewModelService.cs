using AutoMapper;
using BooksStore.Infrastructure;
using BooksStore.Services.Interfaces.IdentityServices;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Сommon.Pagination;
using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Web.Сommon.Services
{
    public class RoleViewModelService : IRoleViewModelService
    {
        private readonly IRoleService _roleManagerService;

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

        public async Task<Result> DeleteAsync(string roleId)
        {
            return await _roleManagerService.DeleteAsync(roleId);
        }

        public async Task<RoleViewModel> FindRoleByIdAsync(string roleId)
        {
            return _mapper.Map<RoleViewModel>(await _roleManagerService.FindRoleByIdAsync(roleId));
        }

        public async Task<IEnumerable<RoleViewModel>> GetRolesAsync(int pageNum)
        {
            if (!PageInfo.PageNumberIsValid(pageNum))
            {
                throw new ArgumentException("Номер страницы не может быть равен или меньше нуля");
            }

            int pageSize = PageSizes.Roles;
            var roles = await _roleManagerService.GetRolesAsync((pageNum - 1) * pageNum, pageSize);

            return _mapper.Map<IEnumerable<RoleViewModel>>(roles);
        }
    }
}
