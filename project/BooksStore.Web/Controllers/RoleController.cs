﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces.Identity;
using BooksStore.Web.Converter._AppUser;
using BooksStore.Web.Converter._Role;
using BooksStore.Web.Models.UpdateModel.Role;
using BooksStore.Web.Models.ViewModels.Index;
using BooksStore.Web.Models.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        IRoleManagerService RoleManagerService { get; set; }
        IUserManagerService UserManagerService { get; set; }
        public RoleController(IRoleManagerService roleManagerService, IUserManagerService userManagerService)
        {
            RoleManagerService = roleManagerService;
            UserManagerService = userManagerService;
        }

        [HttpGet]
        public IActionResult AddRole() => View();
        [HttpPost]
        public async Task<IActionResult> AddRole([Required(ErrorMessage ="Введите названия роли")] string roleName)
        {
            if (ModelState.IsValid)
            {
                await RoleManagerService.CreateRoleAsync(roleName);

                return RedirectToAction("Index", "Role");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> IndexRole(int pageNum = 1)
        {
            if (pageNum >= 1)
            {
                IndexViewModel<RoleViewModel> indexViewModel = new IndexViewModel<RoleViewModel>();

                if ((await RoleManagerService.GetRolesAsync()).Count() != 0)
                {
                    int pageSize = 4;

                    var roles = await RoleManagerService.GetRolesAsync();

                    indexViewModel = new IndexViewModel<RoleViewModel>(pageNum, pageSize, roles.Count(),
                        RoleVMConverter.ConvertToRoleViewModel(roles));
                }
                return View(indexViewModel);
            }

            return BadRequest("Некорректные данные в запросе");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string roleId)
        {
            var result = await RoleManagerService.FindRoleByIdAsync(roleId);
            if(result.Result.Succeeded)
            {
                var members = new List<AppUserDTO>();
                var nonMembers = new List<AppUserDTO>();

                foreach(var user in  UserManagerService.GetAppUsers())
                {
                    var list = await UserManagerService.IsInRoleAsync(user, result.RoleDTO.Name) ? members : nonMembers;
                    list.Add(user);
                }

                return base.View(new RoleEditModel()
                {
                    Memebers = AppUserVMConverter.ConvertToAppUserViewModel(members),
                    NonMembers = AppUserVMConverter.ConvertToAppUserViewModel(nonMembers),
                    RoleViewModel = RoleVMConverter.ConvertToRoleViewModel(result.RoleDTO)
                });
            }
            return View(StatusCode(404));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleUpdateModel updateModel)
        {
            if (ModelState.IsValid)
            {
                foreach(var userId in updateModel.IdsToAdd)
                {
                    var result = await UserManagerService.FindAppUserByIdAsync(userId);
                    if(result.Result.Succeeded)
                    {
                        await UserManagerService.AddToRoleAsync(result.AppUserDTO, updateModel.Name);                            
                    }
                    ModelState.AddModelError("", result.Result.ToString());
                }

                foreach(var userId in updateModel.IdsToDelete)
                {
                    var user = (await UserManagerService.FindAppUserByIdAsync(userId)).AppUserDTO;
                    if(user != null)
                    {
                        await UserManagerService.RemoveFromRoleAsync(user, updateModel.Name);
                    }
                }

                return RedirectToAction("Index", "Role");
            }
            return await Edit(updateModel.Id);
        }

        public async Task<IActionResult> Remove(string roleId)
        {
            var result = await RoleManagerService.FindRoleByIdAsync(roleId);
            if(result.Result.Succeeded)
            {
                await RoleManagerService.DeleteAsync(result.RoleDTO);
            }
            return RedirectToAction("Index" , "Role");
        }       
    }
}