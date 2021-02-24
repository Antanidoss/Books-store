using BooksStore.Web.Сommon.ViewModel.Index;
using System.Collections.Generic;

namespace BooksStore.Web.Сommon.ViewModel.ReadModel
{
    public class BookListViewModel
    {
        public IndexViewModel<BookViewModel> BookIndexModel { get; set; }

        public BookListViewModel(int pageNum, int pageSize, int totalItems, IEnumerable<BookViewModel> books)
        {
            BookIndexModel = new IndexViewModel<BookViewModel>(pageNum, pageSize, totalItems, books);
        }

        public BookListViewModel() { }
    }
}
