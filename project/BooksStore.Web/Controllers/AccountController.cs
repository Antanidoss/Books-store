using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Core.AppUserModel;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Interface.Converter;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Models.Login;
using BooksStore.Web.Models.Registration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Web.Controllers
{
    public class AccountController : Controller
    {
        IAppUserConverter AppUserConverter { get; set; }
        ICurrentUser CurrentUser { get; set; }
        UserManager<AppUser> UserManager { get; set; }
        SignInManager<AppUser> SignInManager { get; set; }
        public AccountController(ICurrentUser currentUser, IAppUserConverter appUserConverter, UserManager<AppUser> userManager,
             SignInManager<AppUser> signInManager)
        {
            CurrentUser = currentUser;
            AppUserConverter = appUserConverter;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel logModel)
        {
            if (ModelState.IsValid)
            {

                var user = await UserManager.FindByEmailAsync(logModel.Email);
                if (user != null)
                {
                    await SignInManager.SignOutAsync();
                    var result = await SignInManager.PasswordSignInAsync(user.UserName, logModel.Password, logModel.IsParsistent, false);

                    if (result.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(user, "user");

                        return !string.IsNullOrEmpty(ViewBag.returnUrl) && Url.IsLocalUrl(ViewBag.returnUrl)
                            ? View(ViewBag.returnUrl)
                            : RedirectToAction("IndexBooks", "Book");
                    }
                    ModelState.AddModelError("", "Пароль или логин введен не верно");
                }
                else
                {
                    ModelState.AddModelError("", "Логин введен не верно");
                }
            }
            return View(logModel);
        }


        [HttpGet]
        public IActionResult Registration() => View();
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel regModel)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(regModel.Email);
                if(user == null)
                {
                    user = new AppUser()
                    {
                        UserName = regModel.Name,
                        Email = regModel.Email
                    };

                    var result = await UserManager.CreateAsync(user, regModel.Password);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, regModel.IsPasrsistent);
                        await UserManager.AddToRoleAsync(user, "user");

                        return !string.IsNullOrEmpty(ViewBag.returnUrl) && Url.IsLocalUrl(ViewBag.returnUrl)
                           ? View(ViewBag.returnUrl)
                           : RedirectToAction("IndexBooks", "Book");

                    }
                    AddErrorsFromResult(result);
                }
                else
                {
                    ModelState.AddModelError("", "Этот логин уже существует");
                }
            }
            return View(regModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("IndexBooks", "Book");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var curUser = (await CurrentUser.GetCurrentAppUser(HttpContext));
            var userViewModel = AppUserConverter.ConvertToAppUserViewModel(curUser);

            userViewModel.RoleName = await UserManager.IsInRoleAsync(curUser , "admin") ? "admin" : "user";

            return View(userViewModel);
        }


        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}