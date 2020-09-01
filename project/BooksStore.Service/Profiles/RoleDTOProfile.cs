using AutoMapper;
using BooksStore.Service.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Service.Profiles
{
    public class RoleDTOProfile : Profile
    {
        public RoleDTOProfile()
        {
            CreateMap<IdentityRole, RoleDTO>();
            CreateMap<RoleDTO, IdentityRole>();
        }
    }
}
