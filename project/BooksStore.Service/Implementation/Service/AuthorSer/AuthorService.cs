using BooksStore.Infastructure.Interfaces;
using BooksStore.Service.Converter;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Service.AuthorSer
{
    public class AuthorService : IAuthorService
    {
        IAuthorRepository AuthorRepository { get; set; }
        public AuthorService(IAuthorRepository authorRepository)
        {
            AuthorRepository = authorRepository;
        }

        public async Task AddAuthorAsync(AuthorDTO authorDTO)
        {
            if(authorDTO != null && authorDTO != default)
            {
                await AuthorRepository.AddAuthorAsync(AuthorDTOConverter.ConvertToAuthor(authorDTO));
            }
        }

        public async Task<AuthorDTO> GetAuthorByIdAsync(int authorId)
        {
            if (authorId >= 1)
            {
                return AuthorDTOConverter.ConvertToAuthorDTO(await AuthorRepository.GetAuthorById(authorId));
            }
            return null;
        }

        public async Task<IEnumerable<AuthorDTO>> GetAuthors(int skip , int take)
        {
            if (skip >= 0 && take >= 1)
            {
                return AuthorDTOConverter.ConvertToAuthorDTO(await AuthorRepository.GetAuthors(skip, take));
            }
            return new List<AuthorDTO>();
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

        public async Task UpdateAuthorAsync(AuthorDTO authorDTO)
        {
            if (authorDTO != null && authorDTO != default)
            {
                await AuthorRepository.UpdateAuthorAsync(AuthorDTOConverter.ConvertToAuthor(authorDTO));
            }
        }

        public async Task<int> GetCountAuthors()
        {
            return await AuthorRepository.GetCountAuthors();
        }
    }
}
