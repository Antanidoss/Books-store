using BooksStore.Core.CommentModel;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Service.Interfaces
{
    public interface ICommentService
    {
        Task AddCommentAsync(CommentDTO commentDTO);
        Task<CommentDTO> GetCommentById(int commentId);
        Task RemoveCommentAsync(int commentId);
        Task<IEnumerable<CommentDTO>> GetComments(int skip, int take);
        Task UpdateCommentAsync(CommentDTO commentDTO);
        Task<IEnumerable<CommentDTO>> GetCommentsByBookId(int bookId);
        Task<int> GetCountComments();
    }
}
