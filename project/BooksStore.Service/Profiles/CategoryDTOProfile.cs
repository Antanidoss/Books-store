using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Services.Profiles
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
