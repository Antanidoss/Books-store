using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using BooksStore.Web.Сommon.ViewModel.UpdateModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Web.Interfaces.Services
{
    public interface IBookViewModelService
    {
        Task AddBookAsync(BookCreateModel bookCreateModel);
        Task<BookViewModel> GetBookByIdAsync(int bookId);
        Task<IEnumerable<BookViewModel>> GetBooksAsync(int pageNum);
        Task<IEnumerable<BookViewModel>> GetBooksByCategoryAsync(int pageNum, int categoryId);
        Task<IEnumerable<BookViewModel>> GetBooksByNameAsync(int pageNum, string bookName);
        Task RemoveBookAsync(int bookId);
        Task UpdateBookAsync(BookUpdateModel bookUpdateModel);
        Task<int> GetCountAsync();
    }
}
