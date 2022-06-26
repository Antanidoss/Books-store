using AutoMapper;
using BooksStore.Common;
using BooksStore.Services.Interfaces.Services;
using BooksStore.Web.Interfaces.Services;
using BooksStore.Web.Сommon.Pagination;
using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Web.Сommon.Services
{
    public class RoleViewService : IRoleViewModelService
    {
        private readonly IRoleService _roleManagerService;

        private readonly IMapper _mapper;

        public RoleViewService(IRoleService roleManagerService, IMapper mapper)
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

        public async Task<IEnumerable<RoleViewModel>> GetRolesAsync(int pageNum)
        {
            var take = PageSizes.Roles;
            var skip = PaginationInfo.GetCountSkipItems(pageNum, take);
            var roles = await _roleManagerService.GetRolesAsync(skip ,take);

            return _mapper.Map<IEnumerable<RoleViewModel>>(roles);
        }
    }
}