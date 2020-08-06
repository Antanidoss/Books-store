using BooksStore.Core.CommentModel;
using BooksStore.Web.Interface.Converter;
using BooksStore.Web.Models.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Converter._Comment
{
    public class CommentConverter : ICommentConverter
    {
        public CommentViewModel ConvertToCommentViewModel(Comment comment)
        {
            if(comment != null)
            {
                CommentViewModel commentViewModel = new CommentViewModel()
                {
                    Id = comment.Id,
                    Descriptions = comment.Descriptions ?? string.Empty,
                    TimeCreate = comment?.TimeOfCreate.ToString() ?? string.Empty,
                    AppUserName = comment.AppUser.UserName ?? "User",
                };
                return commentViewModel;
            }
            return new CommentViewModel();
        }

        public IEnumerable<CommentViewModel> ConvertToCommentViewModel(IEnumerable<Comment> comments)
        {
            if(comments != null && comments.Count() != 0)
            {
                List<CommentViewModel> commentsViewModel = new List<CommentViewModel>();
                foreach(var comment in comments)
                {
                    commentsViewModel.Add(ConvertToCommentViewModel(comment));
                }
                return commentsViewModel;
            }
            return new List<CommentViewModel>();
        }
    }
}
