using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Services.DTO.Category;

namespace BooksStore.Services.Profiles
{
    internal sealed class CategoryDTOProfile : Profile
    {
        public CategoryDTOProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
        }
    }
}