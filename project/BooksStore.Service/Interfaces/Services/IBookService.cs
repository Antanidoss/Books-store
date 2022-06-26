using BooksStore.Services.Models;
using BooksStore.Services.DTO.Book;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.Interfaces
{
    public interface IBookService
    {
        Task AddBookAsync(BookDTOCreateModel bookCreateModel);
        Task<BookDTO> GetBookByIdAsync(int bookId);
        Task RemoveBookAsync(int bookId);
        Task<IEnumerable<BookDTO>> GetBooksAsync(int take, int skip, BookFilterModel filterModel);
        Task UpdateBookAsync(BookDTO book);
        Task<bool> IsBookInBasketAsync(int basketId, int bookId);
        Task<int> GetCountBooksAsync();
    }
}