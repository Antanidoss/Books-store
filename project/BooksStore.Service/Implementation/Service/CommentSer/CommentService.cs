using BooksStore.Core.CommentModel;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Service.Converter;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Service.CommentSer
{
    public class CommentService : ICommentService
    {
        ICommentRepository CommentRepository { get; set; }
        public CommentService(ICommentRepository commentRepository)
        {
            CommentRepository = commentRepository;
        }

        public async Task AddCommentAsync(CommentDTO commentDTO)
        {
            if(commentDTO != null && commentDTO != default)
            {
                await CommentRepository.AddCommentAsync(CommentDTOConverter.ConvertToComment(commentDTO));
            }
        }

        public async Task<CommentDTO> GetCommentById(int commentId)
        {
            if (commentId >= 1)
            {
                return CommentDTOConverter.ConvertToCommentDTO(await CommentRepository.GetCommentById(commentId));
            }
            return null;
        }

        public async Task<IEnumerable<CommentDTO>> GetComments(int skip, int take)
        {
            if (skip >= 0 && take >= 1)
            {
                return CommentDTOConverter.ConvertToCommentDTO(await CommentRepository.GetComments(skip, take));
            }
            return new List<CommentDTO>();
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

        public async Task UpdateCommentAsync(CommentDTO commentDTO)
        {
            if (commentDTO != null && commentDTO != default)
            {
                await CommentRepository.UpdateCommentAsync(CommentDTOConverter.ConvertToComment(commentDTO));
            }
        }

        public async Task<IEnumerable<CommentDTO>> GetCommentsByBookId(int bookId)
        {
            if (bookId >= 1)
            {
                return CommentDTOConverter.ConvertToCommentDTO((await CommentRepository.GetCommentByBookId(bookId) ?? new List<Comment>()));
            }
            return new List<CommentDTO>();
        }

        public async Task<int> GetCountComments()
        {
            return await CommentRepository.GetCountComments();
        }
    }
}
