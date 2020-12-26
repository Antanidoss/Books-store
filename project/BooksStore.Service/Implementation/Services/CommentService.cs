using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO;
using BooksStore.Services.Interfaces;
using BooksStore.Web.CacheOptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IMapper _mapper;

        private readonly ICacheManager _cacheManager;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, ICacheManager cacheManager)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }

        public async Task AddCommentAsync(CommentDTO commentDTO)
        {
            if(commentDTO == null)
            {
                throw new ArgumentNullException(nameof(CommentDTO));
            }
            await _commentRepository.AddCommentAsync(_mapper.Map<Comment>(commentDTO));
        }

        public async Task<CommentDTO> GetCommentById(int commentId)
        {
            if (_cacheManager.IsSet(CacheKeys.GetCommentKey(commentId)))
            {
                return _mapper.Map<CommentDTO>(_cacheManager.Get<Comment>(CacheKeys.GetCommentKey(commentId)));
            }

            if (commentId <= 0)
            {
                throw new ArgumentException("id не может быть равен или меньше нуля");
            }

            var comment = await _commentRepository.GetCommentById(commentId);

            if(comment == null)
            {
                throw new ArgumentNullException(nameof(Comment));
            }

            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<IEnumerable<CommentDTO>> GetComments(int skip, int take)
        {
            if (skip < 0 && take <= 0)
            {
                throw new ArgumentException("Некорректные аргументы skip и take");
            }
            return _mapper.Map<IEnumerable<CommentDTO>>(await _commentRepository.GetComments(skip, take));
        }

        public async Task RemoveCommentAsync(int commentId)
        {
            if (commentId <= 0)
            {
                throw new ArgumentException("id не может быть равен или меньше нуля");
            }

            var comment = await _commentRepository.GetCommentById(commentId);

            if (comment != default)
            {
                await _commentRepository.RemoveCommentAsync(comment);
            }
        }

        public async Task UpdateCommentAsync(CommentDTO commentDTO)
        {
            if (commentDTO == null)
            {
                throw new ArgumentNullException(nameof(CommentDTO));
            }
            await _commentRepository.UpdateCommentAsync(_mapper.Map<Comment>(commentDTO));
            _cacheManager.Remove(CacheKeys.GetCommentKey(commentDTO.Id));
        }

        public async Task<IEnumerable<CommentDTO>> GetCommentsByBookId(int bookId)
        {
            if (bookId <= 0)
            {
                throw new ArgumentNullException(nameof(CommentDTO));
            }
            return _mapper.Map<IEnumerable<CommentDTO>>((await _commentRepository.GetCommentByBookId(bookId) ?? new List<Comment>()));
        }

        public async Task<int> GetCountComments()
        {
            return await _commentRepository.GetCountComments();
        }
    }
}
