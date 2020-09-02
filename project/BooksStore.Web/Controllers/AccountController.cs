using System.Threading.Tasks;
using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces.Identity;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Models.Login;
using BooksStore.Web.Models.Registration;
using BooksStore.Web.Models.ViewModels.AppUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Web.Controllers
{
    public class AccountController : Controller
    {
        ICurrentUser CurrentUser { get; set; }
        IUserManagerService UserManagerService { get; set; }
        IMapper Mapper { get; set; }
        public AccountController(ICurrentUser currentUser, IUserManagerService userManagerService, IMapper mapper)
        {
            CurrentUser = currentUser;      
            UserManagerService = userManagerService;
            Mapper = mapper;
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
                var signInResult = await UserManagerService.PasswordSignInAsync(logModel.Email, logModel.Password, logModel.IsParsistent);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("IndexBooks", "Book");
                }
                ModelState.AddModelError("", signInResult.ToString());                             
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
                var result = await UserManagerService.CreateAppUserAsync(regModel.Name, regModel.Email, regModel.Password);
                if (!result.Result.Succeeded)
                {
                    ModelState.AddModelError("", result.Result.ToString());
                }
                else
                {
                    await UserManagerService.SignInAsync(result.AppUserId, regModel.IsPasrsistent);
                    return RedirectToAction("IndexBooks", "Book");
                }
            }
            return View(regModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await UserManagerService.SignOutAsync();
            return RedirectToAction("IndexBooks", "Book");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AppUserDTO curUser = await CurrentUser.GetCurrentUser(HttpContext);
            var userViewModel = Mapper.Map<AppUserViewModel>(curUser);

            userViewModel.RoleName = await UserManagerService.IsInRoleAsync(curUser , "admin") ? "admin" : "user";

            return View(userViewModel);
        }

    }
}