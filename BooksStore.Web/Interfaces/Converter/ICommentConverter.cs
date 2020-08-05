using BooksStore.Core.CommentModel;
using BooksStore.Web.Models.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Interface.Converter
{
    public interface ICommentConverter
    {
        CommentViewModel ConvertToCommentViewModel(Comment comment);
        IEnumerable<CommentViewModel> ConvertToCommentViewModel(IEnumerable<Comment> comments);
    }
}
