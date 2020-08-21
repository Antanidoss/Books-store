using AutoMapper;
using BooksStore.Core.CategoryModel;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Service.Converter
{
    internal static class CategoryDTOConverter
    {
        public static CategoryDTO ConvertToCategoryDTO(Category category)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            return map.Map<Category, CategoryDTO>(category);
        }
        public static IEnumerable<CategoryDTO> ConvertToCategoryDTO(IEnumerable<Category> categories)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            return map.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);
        }
        public static Category ConvertToCategory(CategoryDTO categoryDTO)
        {
            var map = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, Category>()).CreateMapper();
            return map.Map<CategoryDTO, Category>(categoryDTO);
        }
    }
}
