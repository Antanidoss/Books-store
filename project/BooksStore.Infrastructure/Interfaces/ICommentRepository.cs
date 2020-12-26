using BooksStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces
{
    public interface ICommentRepository
    {
        Task AddCommentAsync(Comment comment);
        Task<Comment> GetCommentById(int id);
        Task RemoveCommentAsync(Comment comment);
        Task<IEnumerable<Comment>> GetComments(int skip, int take);
        Task UpdateCommentAsync(Comment comment);
        Task<IEnumerable<Comment>> GetCommentByBookId(int bookId);
        Task<int> GetCountComments();
    }
}
