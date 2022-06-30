using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO.Comment;
using BooksStore.Services.Interfaces.Services.Base;
using BooksStore.Services.Interfaces.Services.WithCaching;
using BooksStore.Web.CacheOptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.Implementation.Services.WithCaching
{
    internal sealed class CommentCachingService : ICommentCachingService
    {
        private readonly IMapper _mapper;

        private readonly ICacheManager _cacheManager;

        private readonly ICommentService _commentService;

        public CommentCachingService(IMapper mapper, ICacheManager cacheManager, ICommentService commentService)
        {
            _mapper = mapper;
            _cacheManager = cacheManager;
            _commentService = commentService;
        }

        public async Task AddCommentAsync(CommentDTO commentDTO)
        {
            await _commentService.AddCommentAsync(commentDTO);
        }

        public async Task<CommentDTO> GetCommentById(int commentId)
        {
            if (_cacheManager.IsSet(CacheKeys.GetCommentKey(commentId)))
            {
                var cachingComment = _cacheManager.Get<Comment>(CacheKeys.GetCommentKey(commentId));

                return _mapper.Map<CommentDTO>(cachingComment);
            }

            var comment = await _commentService.GetCommentById(commentId);
            _cacheManager.Set<CommentDTO>(CacheKeys.GetCommentKey(commentId), comment, CacheTimes.CommentsCacheTime);

            return comment;
        }

        public async Task<IEnumerable<CommentDTO>> GetComments(int skip, int take, int bookId)
        {
            return await _commentService.GetComments(skip, take, bookId);
        }

        public async Task<int> GetCountComments()
        {
            return await _commentService.GetCountComments();
        }

        public async Task RemoveCommentAsync(int commentId)
        {
            await _commentService.RemoveCommentAsync(commentId);

            _cacheManager.Remove(CacheKeys.GetCommentKey(commentId));
        }

        public async Task UpdateCommentAsync(CommentDTO commentDTO)
        {
            await _commentService.UpdateCommentAsync(commentDTO);

            _cacheManager.Remove(CacheKeys.GetCommentKey(commentDTO.Id));
        }
    }
}
