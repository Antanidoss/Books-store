using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;

namespace BooksStore.Web.Profiles
{
    public class CategoryVMProfile : Profile
    {
        public CategoryVMProfile()
        {
            CreateMap<CategoryDTO, CategoryViewModel>();
            CreateMap<CategoryUpdateModel, CategoryDTO>();
        }
    }
}
