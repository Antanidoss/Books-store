using AutoMapper;
using BooksStore.Core.CategoryModel;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Service.Profiles
{
    public class CategoryDTOProfile : Profile
    {
        public CategoryDTOProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
        }
    }
}
