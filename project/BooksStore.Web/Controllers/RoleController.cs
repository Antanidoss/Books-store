using System.Linq;
using System.Threading.Tasks;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.Index;
using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;
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
        public async Task<IActionResult> AddRole(RoleCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _roleService.CreateRoleAsync(model);

            return RedirectToAction("IndexRole", "Role");
        }

        [HttpGet]
        public async Task<IActionResult> IndexRole(int pageNum = 1)
        {                     
            var roles = await _roleService.GetRolesAsync(pageNum);

            var indexViewModel = new IndexViewModel<RoleViewModel>(pageNum, PageSizes.Roles, roles.Count(), roles);

            return View(indexViewModel);                       
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string roleId)
        {           
            return View(await _roleService.FindRoleByIdAsync(roleId));                  
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return await Edit(model);
            }

            var result = await _roleService.UpdateAsync(model);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(nameof(model), result.Errors.ToString());
                return View(model);
            }
        
            return RedirectToAction("Index", "Role");                        
        }

        public async Task<IActionResult> Remove(string roleId)
        {
            var role = await _roleService.FindRoleByIdAsync(roleId);
            
            await _roleService.DeleteAsync(role);
            
            return RedirectToAction("Index" , "Role");
        }       
    }
}