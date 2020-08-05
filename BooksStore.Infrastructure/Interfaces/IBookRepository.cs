using BooksStore.Core.BookModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces
{
    public interface IBookRepository
    {
        Task AddBookAsync(Book book);
        Task<Book> GetBookByIdAsync(int bookId);
        Task RemoveBookAsync(Book book);
        Task<IEnumerable<Book>> GetBooks(int skip, int take);
        Task UpdateBookAsync(Book book);
        Task<int> GetCountBooks();
    }
}
