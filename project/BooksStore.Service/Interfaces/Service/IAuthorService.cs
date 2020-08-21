using BooksStore.Core.AuthorModel;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Service.Interfaces
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
