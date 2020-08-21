using BooksStore.Core.BookModel;
using BooksStore.Service.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Service.Interfaces
{
    public interface IBookService
    {
        Task AddBookAsync(BookDTO bookDTO);
        Task<BookDTO> GetBookByIdAsync(int bookId);
        Task RemoveBookAsync(int bookId);
        Task<IEnumerable<BookDTO>> GetBooks(int skip, int take);
        Task UpdateBookAsync(BookDTO book);
        Task<bool> IsBookInBasketAsync(int basketId, int bookId);
        Task<IEnumerable<BookDTO>> GetBookByCategoryAsync(int categoryId);
        Task<int> GetCountBooks();
    }
}
