using BooksStore.Core.BookModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Service.Interfaces
{
    public interface IBookService
    {
        Task AddBookAsync(Book book);
        Task<Book> GetBookByIdAsync(int bookId);
        Task RemoveBookAsync(int bookId);
        Task<IEnumerable<Book>> GetBooks(int skip, int take);
        Task UpdateBookAsync(Book book);
        Task<bool> IsBookInBasketAsync(int basketId, int bookId);
        Task<IEnumerable<Book>> GetBookByCategoryAsync(int categoryId);
        Task<int> GetCountBooks();
    }
}
