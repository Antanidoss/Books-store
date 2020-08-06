using BooksStore.Core.CommentModel;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Service.CommentSer
{
    public class CommentService : ICommentService
    {
        ICommentRepository CommentRepository { get; set; }
        public CommentService(ICommentRepository commentRepository) => CommentRepository = commentRepository;

        public async Task AddCommentAsync(Comment comment)
        {
            if(comment != null && comment != default)
            {
                await CommentRepository.AddCommentAsync(comment);
            }
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            if (commentId >= 1)
            {
                return await CommentRepository.GetCommentById(commentId);
            }
            return null;
        }

        public async Task<IEnumerable<Comment>> GetComments(int skip, int take)
        {
            if (skip >= 0 && take >= 1)
            {
                return await CommentRepository.GetComments(skip, take);
            }
            return new List<Comment>();
        }

        public async Task RemoveCommentAsync(int commentId)
        {
            if (commentId >= 1)
            {
                var comment = await CommentRepository.GetCommentById(commentId);

                if (comment != default)
                {
                    await CommentRepository.RemoveCommentAsync(comment);
                }
            }
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            if (comment != null && comment != default)
            {
                await CommentRepository.UpdateCommentAsync(comment);
            }
        }

        public async Task<IEnumerable<Comment>> GetCommentsByBookId(int bookId)
        {
            if (bookId >= 1)
            {
                return (await CommentRepository.GetCommentByBookId(bookId) ?? new List<Comment>());
            }
            return new List<Comment>();
        }

        public async Task<int> GetCountComments()
        {
            return await CommentRepository.GetCountComments();
        }
    }
}
