using AutoMapper;
using BooksStore.Services.DTO.Role;
using Microsoft.AspNetCore.Identity;

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