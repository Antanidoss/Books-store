﻿using BooksStore.Core.AppUserModel;
using BooksStore.Infrastructure;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Service.Interfaces.Identity
{
    public interface IUserManagerService
    {
        Task<(Result Result, string AppUserId)> CreateAppUserAsync(string userName, string email, string password);
        Task<Result> SignInAsync(string appUserId, bool isParsistent);
        Task<(Result Result, AppUserDTO AppUserDTO)> FindAppUserByIdAsync(string appUserId);
        Task<(Result Result, AppUserDTO AppUserDTO)> FindAppUserByEmailAsync(string email);
        Task<Result> RemoveAppUserAsync(string appUserId);
        IEnumerable<AppUserDTO> GetAppUsers();
        Task SignOutAsync();
        Task<string> GetRoleAsync(AppUserDTO appUserDTO);
        Task<Result> AddToRoleAsync(AppUserDTO appUserDTO, string roleName);
        Task<bool> IsInRoleAsync(AppUserDTO appUser, string roleName);
        Task<Result> PasswordSignInAsync(string email, string password, bool isParsistent);
        Task<Result> RemoveFromRoleAsync(AppUserDTO appUserDTO, string roleName);
    }
}
