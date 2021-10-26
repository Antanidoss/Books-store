using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infrastructure;
using BooksStore.Services.DTO;
using BooksStore.Services.DTO.AppUser;
using BooksStore.Services.Interfaces.IdentityServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.Implementation.IdentityServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> SignInManager;

        private readonly IMapper _mapper;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            SignInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<(Result Result, string AppUserId)> CreateAppUserAsync(string userName, string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return (IdentityResultExtensions.EmailAreadyUse(), "");
            }

            user = new AppUser()
            {
                UserName = userName,
                Email = email
            };
            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<(Result Result, AppUserDTO AppUserDTO)> FindAppUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user != null
                ? (Result.Success(), _mapper.Map<AppUserDTO>(user))
                : (IdentityResultExtensions.AppUserNotFound(), new AppUserDTO());
        }

        public async Task<(Result Result, AppUserDTO AppUserDTO)> FindAppUserByIdAsync(string appUserId)
        {
            var user = await _userManager.FindByIdAsync(appUserId);

            return user != null
               ? (Result.Success(), _mapper.Map<AppUserDTO>(user))
               : (IdentityResultExtensions.AppUserNotFound(), new AppUserDTO());
        }

        public async Task SignOutAsync()
        {
            await SignInManager.SignOutAsync();
        }

        public async Task<Result> PasswordSignInAsync(string email, string password, bool isParsistent)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await SignInManager.PasswordSignInAsync(user, password, isParsistent, false);

                return result.Succeeded ? Result.Success() : IdentityResultExtensions.IncorrectlyEnteredEmailOrPassword();
            }

            return IdentityResultExtensions.AppUserNotFound();
        }

        public async Task<Result> RemoveAppUserAsync(string appUserId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(appUserId);
            if (appUser != null)
            {
                var result = await _userManager.DeleteAsync(appUser);
                return result.Succeeded ? Result.Success() : result.ToApplicationResult();
            }

            return IdentityResultExtensions.AppUserNotFound();
        }

        public async Task<Result> SignInAsync(string appUserId, bool isPasrsistent)
        {
            var user = await _userManager.FindByIdAsync(appUserId);
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPasrsistent);
            }

            return (IdentityResultExtensions.AppUserNotFound());
        }

        public async Task<bool> IsInRoleAsync(AppUserDTO appUserDTO, string roleName)
        {
            return await _userManager.IsInRoleAsync(_mapper.Map<AppUser>(appUserDTO), roleName);
        }

        public async Task<Result> RemoveFromRoleAsync(AppUserDTO appUserDTO, string roleName)
        {
            if (appUserDTO != null && appUserDTO != default && !string.IsNullOrEmpty(roleName))
            {
                var result = await _userManager.RemoveFromRoleAsync(_mapper.Map<AppUser>(appUserDTO), roleName);
                return result.Succeeded ? Result.Success() : result.ToApplicationResult();
            }
            return Result.Failure(new string[] { "Некорректные входные данные" });
        }

        public IEnumerable<AppUserDTO> GetAppUsers()
        {
            return _mapper.Map<IEnumerable<AppUserDTO>>(_userManager.Users);
        }

        public Task<Result> AddToRoleAsync(AppUserDTO appUserDTO, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleAsync(AppUserDTO appUserDTO)
        {
            throw new NotImplementedException();
        }
    }
}