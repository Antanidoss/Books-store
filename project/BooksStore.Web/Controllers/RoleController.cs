using System.Linq;
using System.Threading.Tasks;
using BooksStore.Web.Filters;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.Index;
using BooksStore.Web.Models.ViewModel.ReadModel;
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _roleService.CreateRoleAsync(model);

            return RedirectToAction(nameof(IndexRole));
        }

        [HttpGet]
        public async Task<IActionResult> IndexRole(int pageNum = 1)
        {                     
            var roles = await _roleService.GetRolesAsync(pageNum);

            var indexViewModel = new IndexViewModel<RoleViewModel>(pageNum, PageSizes.Roles, roles.Count(), roles);

            return View(indexViewModel);                       
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string roleId)
        {           
            if(string.IsNullOrEmpty(roleId))
            {
                return StatusCode(404);
            }

            await _roleService.DeleteAsync(roleId);
            
            return RedirectToAction(nameof(IndexRole));
        }       
    }
}