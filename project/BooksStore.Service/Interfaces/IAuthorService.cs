using BooksStore.Core.AuthorModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Service.Interfaces
{
    public interface IAuthorService
    {
        Task AddAuthorAsync(Author author);
        Task<Author> GetAuthorByIdAsync(int authorId);
        Task RemoveAuthorAsync(int authorId);
        Task<IEnumerable<Author>> GetAuthors(int skip, int take);
        Task UpdateAuthorAsync(Author author);
        Task<int> GetCountAuthors();
    }
}
