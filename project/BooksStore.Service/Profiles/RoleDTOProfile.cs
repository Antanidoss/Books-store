using AutoMapper;
using BooksStore.Services.DTO.Role;
using Microsoft.AspNetCore.Identity;

namespace BooksStore.Services.Profiles
{
    internal sealed class RoleDTOProfile : Profile
    {
        public RoleDTOProfile()
        {
            CreateMap<IdentityRole, RoleDTO>();
            CreateMap<RoleDTO, IdentityRole>();
        }
    }
}