using BooksStore.Services.DTO.Comment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.Interfaces
{
    public interface ICommentService
    {
        Task AddCommentAsync(CommentDTO commentDTO);
        Task<CommentDTO> GetCommentById(int commentId);
        Task RemoveCommentAsync(int commentId);
        Task<IEnumerable<CommentDTO>> GetComments(int skip, int take, int bookId);
        Task UpdateCommentAsync(CommentDTO commentDTO);
        Task<int> GetCountComments();
    }
}
