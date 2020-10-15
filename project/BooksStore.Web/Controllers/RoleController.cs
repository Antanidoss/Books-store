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
        private readonly IRoleViewModelService _roleViewModelService;

        public RoleController(IRoleViewModelService roleViewModelService)
        {
            _roleViewModelService = roleViewModelService;
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

            await _roleViewModelService.CreateRoleAsync(model);

            return RedirectToAction("IndexRole", "Role");
        }

        [HttpGet]
        public async Task<IActionResult> IndexRole(int pageNum = 1)
        {                     
            var roles = await _roleViewModelService.GetRolesAsync(pageNum);

            var indexViewModel = new IndexViewModel<RoleViewModel>(pageNum, PageSizes.Roles, roles.Count(), roles);

            return View(indexViewModel);                       
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string roleId)
        {           
            return View(await _roleViewModelService.FindRoleByIdAsync(roleId));                  
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleUpdateModel updateModel)
        {
            if (!ModelState.IsValid)
            {
                return await Edit(updateModel.Id);
            }

            var result = await _roleViewModelService.UpdateAsync(updateModel);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(nameof(updateModel), result.Errors.ToString());
                return View(updateModel);
            }
        
            return RedirectToAction("Index", "Role");                        
        }

        public async Task<IActionResult> Remove(string roleId)
        {
            var role = await _roleViewModelService.FindRoleByIdAsync(roleId);
            
            await _roleViewModelService.DeleteAsync(role);
            
            return RedirectToAction("Index" , "Role");
        }       
    }
}