using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Exceptions;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO;
using BooksStore.Services.Interfaces;
using BooksStore.Web.CacheOptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.AuthorSer
{
    public class AuthorService : IAuthorService
    {
        private readonly ICacheManager _cacheManager;

        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IMapper _mapper;

        public AuthorService(IRepositoryFactory repositoryFactory, IMapper mapper, ICacheManager cacheManager)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }

        public async Task AddAuthorAsync(AuthorDTO authorDTO)
        {
            await _repositoryFactory.CreateAuthorRepository().AddAsync(new Author(authorDTO.Firstname, authorDTO.Surname));
        }

        public async Task<AuthorDTO> GetAuthorByIdAsync(int authorId)
        {
            if (_cacheManager.IsSet(CacheKeys.GetAuthorKey(authorId)))
            {
                return _mapper.Map<AuthorDTO>(_cacheManager.Get<Author>(CacheKeys.GetAuthorKey(authorId)));
            }

            var author = await _repositoryFactory.CreateAuthorRepository().GetByIdAsync(authorId);

            if (author == null)
            {
                throw new NotFoundException(nameof(Author), author);
            }

            _cacheManager.Set<Author>(CacheKeys.GetAuthorKey(author.Id), author, CacheTimes.AuthorCacheTime);
            return _mapper.Map<AuthorDTO>(author);
        }

        public async Task<IEnumerable<AuthorDTO>> GetAuthors(int skip, int take)
        {
            return _mapper.Map<IEnumerable<AuthorDTO>>(await _repositoryFactory.CreateAuthorRepository().GetAsync(skip, take));
        }

        public async Task RemoveAuthorAsync(int authorId)
        {
            if (authorId >= 1)
            {
                var author = await _repositoryFactory.CreateAuthorRepository().GetByIdAsync(authorId);
                if (author != null)
                {
                    await _repositoryFactory.CreateAuthorRepository().RemoveAsync(author);
                    _cacheManager.Remove(CacheKeys.GetAuthorKey(author.Id));
                }
            }
        }

        public async Task UpdateAuthorAsync(AuthorDTO authorDTO)
        {
            if (authorDTO != null && authorDTO != default)
            {
                await _repositoryFactory.CreateAuthorRepository().UpdateAsync(_mapper.Map<Author>(authorDTO));
                _cacheManager.Remove(CacheKeys.GetAuthorKey(authorDTO.Id));
            }
        }

        public async Task<int> GetCountAuthors()
        {
            return await _repositoryFactory.CreateAuthorRepository().GetCountAsync();
        }
    }
}