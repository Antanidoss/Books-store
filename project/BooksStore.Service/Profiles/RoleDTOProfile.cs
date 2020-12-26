using AutoMapper;
using BooksStore.Services.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Services.Profiles
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
