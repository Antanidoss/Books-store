using AutoMapper;
using BooksStore.Infrastructure;
using BooksStore.Services.DTO;
using BooksStore.Services.Interfaces.Identity;
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
