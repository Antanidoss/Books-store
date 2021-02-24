using AutoMapper;
using BooksStore.Services.DTO;
using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;

namespace BooksStore.Web.Сommon.Profiles
{
    public class CommentVMProfile : Profile
    {
        public CommentVMProfile()
        {
            CreateMap<CommentDTO, CommentViewModel>();
            CreateMap<CommentCreateModel, CommentDTO>();
        }
    }
}
