using System.Linq;
using System.Threading.Tasks;
using BooksStore.Web.Filters;
using BooksStore.Web.Interfaces.Services;
using BooksStore.Web.Сommon.Pagination;
using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.Index;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private readonly IRoleViewModelService _roleService;

        public RoleController(IRoleViewModelService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult AddRole() => View();
        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<IActionResult> AddRole(RoleCreateModel model)
        {
            await _roleService.CreateRoleAsync(model);

            return RedirectToAction(nameof(IndexRole));
        }

        [HttpGet]
        [PageNumValidationFilter]
        public async Task<IActionResult> IndexRole(int pageNum = 1)
        {
            var roles = await _roleService.GetRolesAsync(pageNum);

            return View(new IndexViewModel<RoleViewModel>(pageNum, PageSizes.Roles, roles.Count(), roles));
        }

        [HttpPost]
        [IdValidationFilter("roleId")]
        public async Task<IActionResult> RemoveRole(string roleId)
        {
            await _roleService.DeleteAsync(roleId);

            return RedirectToAction(nameof(IndexRole));
        }
    }
}