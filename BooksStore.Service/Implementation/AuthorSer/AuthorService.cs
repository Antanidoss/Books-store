using BooksStore.Core.AuthorModel;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Service.AuthorSer
{
    public class AuthorService : IAuthorService
    {
        IAuthorRepository AuthorRepository { get; set; }
        public AuthorService(IAuthorRepository authorRepository) => AuthorRepository = authorRepository;

        public async Task AddAuthorAsync(Author author)
        {
            if(author != null && author != default)
            {
                await AuthorRepository.AddAuthorAsync(author);
            }
        }

        public async Task<Author> GetAuthorByIdAsync(int authorId)
        {
            if (authorId >= 1)
            {
                return await AuthorRepository.GetAuthorById(authorId);
            }
            return null;
        }

        public async Task<IEnumerable<Author>> GetAuthors(int skip , int take)
        {
            if (skip >= 0 && take >= 1)
            {
                return await AuthorRepository.GetAuthors(skip, take);
            }
            return new List<Author>();
        }

        public async Task RemoveAuthorAsync(int authorId)
        {
            if (authorId >= 1)
            {
                var author = await AuthorRepository.GetAuthorById(authorId);
                if (author != null)
                {
                    await AuthorRepository.RemoveAuthorAsync(author);
                }
            }
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            if (author != null && author != default)
            {
                await AuthorRepository.UpdateAuthorAsync(author);
            }
        }

        public async Task<int> GetCountAuthors()
        {
            return await AuthorRepository.GetCountAuthors();
        }
    }
}
