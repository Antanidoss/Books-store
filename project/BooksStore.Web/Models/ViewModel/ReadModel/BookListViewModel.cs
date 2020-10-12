using BooksStore.Web.Models.ViewModel.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.ViewModel.ReadModel
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
