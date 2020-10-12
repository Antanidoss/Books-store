using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Interfaces.Managers
{
    public interface ICommentManager
    {
        Task AddComment(CommentCreateModel model);
        Task<CommentViewModel> GetCommentById(int commentId);
        Task<IEnumerable<CommentViewModel>> GetComments(int pageNum);
        Task RemoveComment(int commentId);
        Task UpdateComment(CommentUpdateModel model);
        Task<IEnumerable<CommentViewModel>> GetCommentsByBookId(int bookId);
        Task<int> GetCountComments(int bookId);
    }
}
