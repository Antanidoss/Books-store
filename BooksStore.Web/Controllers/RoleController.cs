using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Core.AppUserModel;
using BooksStore.Web.Interface;
using BooksStore.Web.Interface.Converter;
using BooksStore.Web.Models.UpdateModel.Role;
using BooksStore.Web.Models.ViewModels.Index;
using BooksStore.Web.Models.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> RoleManager { get; set; }
        UserManager<AppUser> UserManager { get; set; }
        IRoleConverter RoleConverter { get; set; }
        IAppUserConverter AppUserConverter { get; set; }
        public RoleController(RoleManager<IdentityRole> roleManager , UserManager<AppUser> userManager, IRoleConverter roleConverter,
            IAppUserConverter appUserConverter)
        {
            RoleManager = roleManager;
            UserManager = userManager;
            RoleConverter = roleConverter;
            AppUserConverter = appUserConverter;
        }

        [HttpGet]
        public IActionResult AddRole() => View();
        [HttpPost]
        public async Task<IActionResult> AddRole([Required(ErrorMessage ="Введите названия роли")] string roleName)
        {
            if (ModelState.IsValid)
            {
                await RoleManager.CreateAsync(new IdentityRole() { Name = roleName });

                return RedirectToAction("Index", "Role");
            }
            return View();
        }

        [HttpGet]
        public IActionResult IndexRole(int pageNum = 1)
        {
            if (pageNum >= 1)
            {
                IndexViewModel<RoleViewModel> indexViewModel = new IndexViewModel<RoleViewModel>();

                if (RoleManager.Roles.Count() != 0)
                {
                    int pageSize = 4;

                    var roles = RoleManager.Roles;

                    indexViewModel = new IndexViewModel<RoleViewModel>(pageNum, pageSize, roles.Count(),
                        RoleConverter.ConvertToRoleViewModel(roles));
                }
                return View(indexViewModel);
            }

            return BadRequest("Некорректные данные в запросе");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string roleId)
        {
            var role = await RoleManager.FindByIdAsync(roleId);
            if(role != null)
            {
                var members = new List<AppUser>();
                var nonMembers = new List<AppUser>();

                foreach(var user in UserManager.Users)
                {
                    var list = await UserManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                    list.Add(user);
                }

                return View(new RoleEditModel()
                {
                    Memebers = AppUserConverter.ConvertToAppUserViewModel(members as IQueryable<AppUser>),
                    NonMembers = AppUserConverter.ConvertToAppUserViewModel(nonMembers as IQueryable<AppUser>),
                    RoleViewModel = RoleConverter.ConvertToRoleViewModel(role)
                });
            }
            return View(StatusCode(404));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleUpdateModel updateModel)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach(var userId in updateModel.IdsToAdd)
                {
                    var user = await UserManager.FindByIdAsync(userId);
                    if(user != null)
                    {
                        result = await UserManager.AddToRoleAsync(user, updateModel.Name);

                        if (!result.Succeeded)
                            AddErrorsFromResult(result);
                    }
                }

                foreach(var userId in updateModel.IdsToDelete)
                {
                    var user = await UserManager.FindByIdAsync(userId);
                    if(user != null)
                    {
                        result = await UserManager.RemoveFromRoleAsync(user, updateModel.Name);

                        if (!result.Succeeded)
                            AddErrorsFromResult(result);
                    }
                }

                return RedirectToAction("Index", "Role");
            }
            return await Edit(updateModel.Id);
        }

        public async Task<IActionResult> Remove(string roleId)
        {
            var removeRole = await RoleManager.FindByIdAsync(roleId);
            if(removeRole != null)
            {
                var result = await RoleManager.DeleteAsync(removeRole);

                if (!result.Succeeded)
                    AddErrorsFromResult(result);
            }
            return RedirectToAction("Index" , "Role");
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}