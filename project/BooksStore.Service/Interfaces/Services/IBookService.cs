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
        Task<IEnumerable<BookDTO>> GetBooks(int skip, int take);
        Task UpdateBookAsync(BookDTO book);
        Task<bool> IsBookInBasketAsync(int basketId, int bookId);
        Task<IEnumerable<BookDTO>> GetBooksByCategoryAsync(int take, int skip, int categoryId);
        Task<int> GetCountBooks();
        Task<IEnumerable<BookDTO>> GetBooksByNameAsync(int skip, int take, string bookName);
    }
}
