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
            await _commentRepository.AddCommentAsync(_mapper.Map<Comment>(commentDTO));
        }

        public async Task<CommentDTO> GetCommentById(int commentId)
        {
            if (_cacheManager.IsSet(CacheKeys.GetCommentKey(commentId)))
            {
                return _mapper.Map<CommentDTO>(_cacheManager.Get<Comment>(CacheKeys.GetCommentKey(commentId)));
            }            

            var comment = await _commentRepository.GetCommentById(commentId);

            if (comment == null)
            {
                throw new ArgumentNullException(nameof(Comment));
            }

            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<IEnumerable<CommentDTO>> GetComments(int skip, int take)
        {
            var comments = await _commentRepository.GetComments(skip, take);

            return _mapper.Map<IEnumerable<CommentDTO>>(comments);
        }

        public async Task RemoveCommentAsync(int commentId)
        {
            var comment = await _commentRepository.GetCommentById(commentId);

            if (comment == null)
            {
                throw new ArgumentNullException(nameof(Comment));              
            }

            await _commentRepository.RemoveCommentAsync(comment);
        }

        public async Task UpdateCommentAsync(CommentDTO commentDTO)
        {
            await _commentRepository.UpdateCommentAsync(_mapper.Map<Comment>(commentDTO));
            _cacheManager.Remove(CacheKeys.GetCommentKey(commentDTO.Id));
        }

        public async Task<IEnumerable<CommentDTO>> GetCommentsByBookId(int bookId)
        {
            var comments = await _commentRepository.GetCommentByBookId(bookId) ?? new List<Comment>();

            return _mapper.Map<IEnumerable<CommentDTO>>(comments);
        }

        public async Task<int> GetCountComments()
        {
            return await _commentRepository.GetCountComments();
        }
    }
}
