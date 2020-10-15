using System.Threading.Tasks;
using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces.Identity;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICurrentUser _currentUser;

        private readonly IUserService _userManagerService;

        private readonly IMapper _mapper;

        public IMapper Mapper => _mapper;

        public AccountController(ICurrentUser currentUser, IUserService userManagerService, IMapper mapper)
        {
            _currentUser = currentUser;      
            _userManagerService = userManagerService;
            _mapper = mapper;
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
                var signInResult = await _userManagerService.PasswordSignInAsync(logModel.Email, logModel.Password, logModel.IsParsistent);
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
                var result = await _userManagerService.CreateAppUserAsync(regModel.Name, regModel.Email, regModel.Password);
                if (!result.Result.Succeeded)
                {
                    ModelState.AddModelError("", result.Result.ToString());
                    return View(regModel);
                }
                await _userManagerService.SignInAsync(result.AppUserId, regModel.IsPasrsistent);
                return RedirectToAction("IndexBooks", "Book");
            }
            return View(regModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _userManagerService.SignOutAsync();
            return RedirectToAction("IndexBooks", "Book");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AppUserDTO curUser = await _currentUser.GetCurrentUser(HttpContext);
            var userViewModel = _mapper.Map<AppUserViewModel>(curUser);

            userViewModel.RoleName = await _userManagerService.IsInRoleAsync(curUser , "admin") ? "admin" : "user";

            return View(userViewModel);
        }
    }
}