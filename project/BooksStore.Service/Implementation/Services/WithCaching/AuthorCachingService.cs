using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Services.CacheOptions;
using BooksStore.Services.DTO.Author;
using BooksStore.Services.Interfaces;
using BooksStore.Services.Interfaces.Services.Base;
using BooksStore.Services.Interfaces.Services.WithCaching;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.Implementation.Services.WithCaching
{
    internal sealed class AuthorCachingService : IAuthorCachingService
    {
        private readonly IAuthorService _authorService;

        private readonly ICacheManager _cacheManager;

        private readonly IMapper _mapper;

        public AuthorCachingService(IAuthorService authorService, ICacheManager cacheManager, IMapper mapper)
        {
            _authorService = authorService;
            _cacheManager = cacheManager;
            _mapper = mapper;
        }

        public async Task AddAuthorAsync(AuthorDTO authorDTO)
        {
            await _authorService.AddAuthorAsync(authorDTO);
        }

        public async Task<AuthorDTO> GetAuthorByIdAsync(int authorId)
        {
            if (_cacheManager.IsSet(CacheKeys.GetAuthorKey(authorId)))
                return _mapper.Map<AuthorDTO>(_cacheManager.Get<Author>(CacheKeys.GetAuthorKey(authorId)));

            var author = await _authorService.GetAuthorByIdAsync(authorId);
            _cacheManager.Set<AuthorDTO>(CacheKeys.GetAuthorKey(author.Id), author, CacheTimes.AuthorCacheTime);

            return author;
        }

        public async Task<IEnumerable<AuthorDTO>> GetAuthors(int skip, int take)
        {
            return await _authorService.GetAuthors(skip, take);
        }

        public async Task<int> GetCountAuthors()
        {
            return await _authorService.GetCountAuthors();
        }

        public async Task RemoveAuthorAsync(int authorId)
        {
            await _authorService.RemoveAuthorAsync(authorId);
            _cacheManager.Remove(CacheKeys.GetAuthorKey(authorId));
        }

        public async Task UpdateAuthorAsync(AuthorDTO authorDTO)
        {
            await _authorService.UpdateAuthorAsync(authorDTO);
            _cacheManager.Remove(CacheKeys.GetAuthorKey(authorDTO.Id));
        }
    }
}
