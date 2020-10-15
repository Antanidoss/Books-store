using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Interfaces.Managers
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
