using AutoMapper;
using BooksStore.Services.DTO.Category;
using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using BooksStore.Web.Сommon.ViewModel.UpdateModel;

namespace BooksStore.Web.Сommon.Profiles
{
    public class CategoryVMProfile : Profile
    {
        public CategoryVMProfile()
        {
            CreateMap<CategoryDTO, CategoryViewModel>();
            CreateMap<CategoryUpdateModel, CategoryDTO>();
            CreateMap<CategoryCreateModel, CategoryDTO>();
        }
    }
}
