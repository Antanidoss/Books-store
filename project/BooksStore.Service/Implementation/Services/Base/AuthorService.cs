using AutoMapper;
using BooksStore.Common.Exceptions;
using BooksStore.Core.Entities;
using BooksStore.Services.DTO.Author;
using BooksStore.Services.Implementation.Filters.AuthorFilters;
using BooksStore.Services.Interfaces.Repositories;
using BooksStore.Services.Interfaces.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.Implementation.Services.Base
{
    internal sealed class AuthorService : IAuthorService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IMapper _mapper;

        public AuthorService(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        public async Task AddAuthorAsync(AuthorDTO authorDTO)
        {
            await _repositoryFactory.CreateAuthorRepository().AddAsync(new Author(authorDTO.Firstname, authorDTO.Surname));
        }

        public async Task<AuthorDTO> GetAuthorByIdAsync(int authorId)
        {
            var filter = new AuthorByIdFilterSpec(authorId);
            var author = await _repositoryFactory.CreateAuthorRepository().GetAsync(filter);

            if (author == null)
                throw new NotFoundException(nameof(Author), author);

            return _mapper.Map<AuthorDTO>(author);
        }

        public async Task<IEnumerable<AuthorDTO>> GetAuthors(int skip, int take)
        {
            return _mapper.Map<IEnumerable<AuthorDTO>>(await _repositoryFactory.CreateAuthorRepository().GetAsync(skip, take));
        }

        public async Task RemoveAuthorAsync(int authorId)
        {
            var filter = new AuthorByIdFilterSpec(authorId);
            var author = await _repositoryFactory.CreateAuthorRepository().GetAsync(filter);

            if (author != null)
                await _repositoryFactory.CreateAuthorRepository().RemoveAsync(author);
        }

        public async Task UpdateAuthorAsync(AuthorDTO authorDTO)
        {
            if (authorDTO != null)
                await _repositoryFactory.CreateAuthorRepository().UpdateAsync(_mapper.Map<Author>(authorDTO));
        }

        public async Task<int> GetCountAuthors()
        {
            return await _repositoryFactory.CreateAuthorRepository().GetCountAsync();
        }
    }
}