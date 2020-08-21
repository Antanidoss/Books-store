using AutoMapper;
using BooksStore.Service.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Service.Converter
{
    internal static class RoleDTOConverter
    {
        public static RoleDTO ConvertToRoleDTO(IdentityRole role)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<IdentityRole, RoleDTO>()).CreateMapper();
            return map.Map<IdentityRole, RoleDTO>(role);
        }
        public static IEnumerable<RoleDTO> ConvertToRoleDTO(IEnumerable<IdentityRole> roles)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<IdentityRole, RoleDTO>()).CreateMapper();
            return map.Map<IEnumerable<IdentityRole>, IEnumerable<RoleDTO>>(roles);
        }
        public static IdentityRole ConvertToRole(RoleDTO roleDTO)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, IdentityRole>()).CreateMapper();
            return map.Map<RoleDTO, IdentityRole>(roleDTO);
        }
    }
}
