using BooksStore.Core.CommentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Service.Interfaces
{
    public interface ICommentService
    {
        Task AddCommentAsync(Comment comment);
        Task<Comment> GetCommentById(int commentId);
        Task RemoveCommentAsync(int commentId);
        Task<IEnumerable<Comment>> GetComments(int skip, int take);
        Task UpdateCommentAsync(Comment comment);
        Task<IEnumerable<Comment>> GetCommentsByBookId(int bookId);
        Task<int> GetCountComments();
    }
}
