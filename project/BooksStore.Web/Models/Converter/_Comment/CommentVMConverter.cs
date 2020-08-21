using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Web.Models.ViewModels.Comment;
using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Web.Converter._Comment
{
    public static class CommentVMConverter
    {
        public static CommentViewModel ConvertToCommentViewModel(CommentDTO commentDTO)
        {
            if(commentDTO != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, CommentViewModel>()).CreateMapper();

                return mapper.Map<CommentDTO, CommentViewModel>(commentDTO);
            }

            return new CommentViewModel();
        }

        public static IEnumerable<CommentViewModel> ConvertToCommentViewModel(IEnumerable<CommentDTO> commentsDTO)
        {
            if(commentsDTO != null && commentsDTO.Count() != 0)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, CommentViewModel>()).CreateMapper();

                return mapper.Map<IEnumerable<CommentDTO>, IEnumerable<CommentViewModel>>(commentsDTO);
            }

            return new List<CommentViewModel>();
        }
    }
}
