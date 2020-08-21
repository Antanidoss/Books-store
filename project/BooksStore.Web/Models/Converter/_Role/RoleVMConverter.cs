using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Web.Models.ViewModels.Role;
using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Web.Converter._Role
{
    public static class RoleVMConverter
    {
        public static RoleViewModel ConvertToRoleViewModel(RoleDTO roleDTO)
        {
            if(roleDTO != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, RoleViewModel>()).CreateMapper();
                return mapper.Map<RoleDTO, RoleViewModel>(roleDTO);
            }

            return new RoleViewModel();
        }

        public static IEnumerable<RoleViewModel> ConvertToRoleViewModel(IEnumerable<RoleDTO> rolesDTO)
        {
            if(rolesDTO != null && rolesDTO.Count() != 0)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, RoleViewModel>()).CreateMapper();
                return mapper.Map<IEnumerable<RoleDTO>, IEnumerable<RoleViewModel>>(rolesDTO);
            }

            return new List<RoleViewModel>();
        }
    }
}
