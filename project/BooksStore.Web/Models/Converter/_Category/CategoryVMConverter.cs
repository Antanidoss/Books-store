using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Web.Models.ViewModels.Category;
using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Web.Converter._Category
{
    public static class CategoryVMConverter
    {
        public static CategoryViewModel ConvertToCategoryViewModel(CategoryDTO categoryDTO)
        {
            if(categoryDTO != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
                return mapper.Map<CategoryDTO, CategoryViewModel>(categoryDTO);
            }

            return new CategoryViewModel();
        }

        public static IEnumerable<CategoryViewModel> ConvertToCategoryViewModel(IEnumerable<CategoryDTO> categoriesDTO)
        {
            if(categoriesDTO != null && categoriesDTO.Count() != 0)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
                return mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryViewModel>>(categoriesDTO);
            }

            return new List<CategoryViewModel>();
        }
    }
}
