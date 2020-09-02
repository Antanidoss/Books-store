using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Web.Models.ViewModels.Category;
using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Web.Profiles
{
    public class CategoryVMProfile : Profile
    {
        public CategoryVMProfile()
        {
            CreateMap<CategoryDTO, CategoryViewModel>();
        }
    }
}
