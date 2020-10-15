using AutoMapper;
using BooksStore.Core.AppUserModel;
using BooksStore.Infrastructure;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Service.Implementation.Identity
{
    public class UserService : IUserService
    {
        UserManager<AppUser> UserManager { get; set; }
        SignInManager<AppUser> SignInManager { get; set; }
        IMapper Mapper { get; set; }
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            Mapper = mapper;
        }

        public async Task<(Result Result, string AppUserId)> CreateAppUserAsync(string userName, string email, string password)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new AppUser()
                {
                    UserName = userName,
                    Email = email
                };
                
                var result = await UserManager.CreateAsync(user, password);
                
                return (result.ToApplicationResult(), user.Id);
            }
            return (IdentityResultExtensions.EmailAreadyUse(), "");
        }

        public async Task<(Result Result, AppUserDTO AppUserDTO)> FindAppUserByEmailAsync(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);

            return user != null 
                ? (Result.Success(), Mapper.Map<AppUserDTO>(user)) 
                : (IdentityResultExtensions.AppUserNotFound(), new AppUserDTO());
        }

        public async Task<(Result Result, AppUserDTO AppUserDTO)> FindAppUserByIdAsync(string appUserId)
        {
            var user = await UserManager.FindByIdAsync(appUserId);

            return user != null
               ? (Result.Success(), Mapper.Map<AppUserDTO>(user))
               : (IdentityResultExtensions.AppUserNotFound(), new AppUserDTO());
        }

        public async Task SignOutAsync()
        {
            await SignInManager.SignOutAsync();
        }

        public async Task<Result> PasswordSignInAsync(string email, string password, bool isParsistent)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await SignInManager.PasswordSignInAsync(user, password, isParsistent, false);

                return result.Succeeded ? Result.Success() : IdentityResultExtensions.IncorrectlyEnteredEmailOrPassword();
            }

            return IdentityResultExtensions.AppUserNotFound();
        }

        public async Task<Result> RemoveAppUserAsync(string appUserId)
        {
            Core.AppUserModel.AppUser appUser = await UserManager.FindByIdAsync(appUserId);
            if(appUser != null)
            {
                var result = await UserManager.DeleteAsync(appUser);
                return result.Succeeded ? Result.Success() : result.ToApplicationResult();
            }

            return IdentityResultExtensions.AppUserNotFound();
        }

        public async Task<Result> SignInAsync(string appUserId, bool isPasrsistent)
        {
            var user = await UserManager.FindByIdAsync(appUserId);
            if(user != null)
            {
                await SignInManager.SignInAsync(user, isPasrsistent);
            }

            return (IdentityResultExtensions.AppUserNotFound());
        }

        public async Task<bool> IsInRoleAsync(AppUserDTO appUserDTO, string roleName)
        {
            return await UserManager.IsInRoleAsync(Mapper.Map<AppUser>(appUserDTO), roleName);
        }

        public async Task<Result> RemoveFromRoleAsync(AppUserDTO appUserDTO, string roleName)
        {
            if(appUserDTO != null && appUserDTO != default && !string.IsNullOrEmpty(roleName))
            {
                var result = await UserManager.RemoveFromRoleAsync(Mapper.Map<AppUser>(appUserDTO), roleName);
                return result.Succeeded ? Result.Success() : result.ToApplicationResult();
            }
            return Result.Failure(new string[] { "Некорректные входные данные" });
        }

        public IEnumerable<AppUserDTO> GetAppUsers()
        {
            return Mapper.Map<IEnumerable<AppUserDTO>>(UserManager.Users);
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
