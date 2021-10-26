using BooksStore.Services.DTO.Author;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.Interfaces
{
    public interface IAuthorService
    {
        Task AddAuthorAsync(AuthorDTO authorDTO);
        Task<AuthorDTO> GetAuthorByIdAsync(int authorId);
        Task RemoveAuthorAsync(int authorId);
        Task<IEnumerable<AuthorDTO>> GetAuthors(int skip, int take);
        Task UpdateAuthorAsync(AuthorDTO authorDTO);
        Task<int> GetCountAuthors();
    }
}
