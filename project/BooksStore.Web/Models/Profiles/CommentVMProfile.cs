using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Web.Models.ViewModels.Comment;
using System.Collections.Generic;
using System.Linq;

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
