using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Web.Models.ViewModel.ReadModel;

namespace BooksStore.Web.Profiles
{
    public class CommentVMProfile : Profile
    {
        public CommentVMProfile()
        {
            CreateMap<CommentDTO, CommentViewModel>();
        }
    }
}
