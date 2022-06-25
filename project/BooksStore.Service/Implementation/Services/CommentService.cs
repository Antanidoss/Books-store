using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO.Comment;
using BooksStore.Services.Implementation.Filters.CommentFilters;
using BooksStore.Services.Interfaces;
using BooksStore.Web.CacheOptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services
{
    internal sealed class CommentService : ICommentService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IMapper _mapper;

        private readonly ICacheManager _cacheManager;

        public CommentService(IRepositoryFactory repositoryFactory, IMapper mapper, ICacheManager cacheManager)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }

        public async Task AddCommentAsync(CommentDTO commentDTO)
        {
            var comment = new Comment(commentDTO.Descriptions, commentDTO.BookId, commentDTO.AppUserId);
            await _repositoryFactory.CreateCommentRepository().AddAsync(comment);
        }

        public async Task<CommentDTO> GetCommentById(int commentId)
        {
            if (_cacheManager.IsSet(CacheKeys.GetCommentKey(commentId)))
            {
                return _mapper.Map<CommentDTO>(_cacheManager.Get<Comment>(CacheKeys.GetCommentKey(commentId)));
            }

            var comment = await _repositoryFactory.CreateCommentRepository().GetAsync(new CommentByIdFilterSpec(commentId));
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(Comment));
            }

            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<IEnumerable<CommentDTO>> GetComments(int skip, int take, int bookId)
        {
            var comments = await _repositoryFactory.CreateCommentRepository().GetAsync(skip, take, new CommentByBookIdFilterSpec(bookId));

            return _mapper.Map<IEnumerable<CommentDTO>>(comments);
        }

        public async Task RemoveCommentAsync(int commentId)
        {
            var comment = await _repositoryFactory.CreateCommentRepository().GetAsync(new CommentByIdFilterSpec(commentId));

            if (comment == null)
            {
                throw new ArgumentNullException(nameof(Comment));
            }

            await _repositoryFactory.CreateCommentRepository().RemoveAsync(comment);
        }

        public async Task UpdateCommentAsync(CommentDTO commentDTO)
        {
            await _repositoryFactory.CreateCommentRepository().UpdateAsync(_mapper.Map<Comment>(commentDTO));
            _cacheManager.Remove(CacheKeys.GetCommentKey(commentDTO.Id));
        }

        public async Task<int> GetCountComments()
        {
            return await _repositoryFactory.CreateCommentRepository().GetCountAsync();
        }
    }
}