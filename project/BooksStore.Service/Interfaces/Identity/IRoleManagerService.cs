﻿using BooksStore.Infrastructure;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Service.Interfaces.Identity
{
    public interface IRoleManagerService
    {
        Task<Result> CreateRoleAsync(string roleName);
        Task<IEnumerable<RoleDTO>> GetRolesAsync(int skip, int take);
        Task<RoleDTO> FindRoleByIdAsync(string roleId);
        Task<Result> DeleteAsync(RoleDTO roleDTO);
    }
}
