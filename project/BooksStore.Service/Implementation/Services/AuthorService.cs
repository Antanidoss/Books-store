using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Infrastructure.Exceptions;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO;
using BooksStore.Services.Interfaces;
using BooksStore.Web.CacheOptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.AuthorSer
{
    public class AuthorService : IAuthorService
    {
        private readonly ICacheManager _cacheManager;

        private readonly IAuthorRepository _authorRepository;

        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper, ICacheManager cacheManager)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }

        public async Task AddAuthorAsync(AuthorDTO authorDTO)
        {
            if(authorDTO != null && authorDTO != default)
            {
                await _authorRepository.AddAuthorAsync(_mapper.Map<Author>(authorDTO));
            }
        }

        public async Task<AuthorDTO> GetAuthorByIdAsync(int authorId)
        {
            if(_cacheManager.IsSet(CacheKeys.GetAuthorKey(authorId)))
            {
                return _mapper.Map<AuthorDTO>(_cacheManager.Get<Author>(CacheKeys.GetAuthorKey(authorId)));
            }

            if (authorId <= 0)
            {
                return null;
            }

            var author = await _authorRepository.GetAuthorById(authorId);

            if(author == null)
            {
                throw new NotFoundException(nameof(Author), author);
            }

            _cacheManager.Set<Author>(CacheKeys.GetAuthorKey(author.Id), author, CacheTimes.AuthorCacheTime);
            return _mapper.Map<AuthorDTO>(author);
        }

        public async Task<IEnumerable<AuthorDTO>> GetAuthors(int skip , int take)
        {
            if (skip < 0 && take <= 0)
            {
                throw new ArgumentException("Некорректные аргументы skip и take");
            }
            return _mapper.Map<IEnumerable<AuthorDTO>>(await _authorRepository.GetAuthors(skip, take));
        }

        public async Task RemoveAuthorAsync(int authorId)
        {
            if (authorId >= 1)
            {
                var author = await _authorRepository.GetAuthorById(authorId);
                if (author != null)
                {
                    await _authorRepository.RemoveAuthorAsync(author);
                    _cacheManager.Remove(CacheKeys.GetAuthorKey(author.Id));
                }
            }
        }

        public async Task UpdateAuthorAsync(AuthorDTO authorDTO)
        {
            if (authorDTO != null && authorDTO != default)
            {
                await _authorRepository.UpdateAuthorAsync(_mapper.Map<Author>(authorDTO));
                _cacheManager.Remove(CacheKeys.GetAuthorKey(authorDTO.Id));
            }
        }

        public async Task<int> GetCountAuthors()
        {
            return await _authorRepository.GetCountAuthors();
        }
    }
}
