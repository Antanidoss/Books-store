using BooksStore.Core.AuthorModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces
{
    public interface IAuthorRepository
    {
        Task AddAuthorAsync(Author author);
        Task<Author> GetAuthorById(int authorId);
        Task RemoveAuthorAsync(Author author);
        Task<IEnumerable<Author>> GetAuthors(int skip, int take);
        Task UpdateAuthorAsync(Author author);
        Task<Author> GetAuthorByName(string firstName, string surname);
        Task<int> GetCountAuthors();
    }
}
