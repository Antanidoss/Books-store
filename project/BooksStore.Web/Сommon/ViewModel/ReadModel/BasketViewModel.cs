using BooksStore.Web.Сommon.ViewModel.Index;
using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Web.Сommon.ViewModel.ReadModel
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public IndexViewModel<BookViewModel> BookIndexModel { get; set; }
        public decimal TotalPrice
        {
            get { return BookIndexModel.Objects.Sum(p => p.Price); }
        }

        public BasketViewModel(int pageNum, int pageSize, int totalItems, int basketId, IEnumerable<BookViewModel> books)
        {
            Id = basketId;
            BookIndexModel = new IndexViewModel<BookViewModel>(pageNum, pageSize, totalItems, books);
        }

    }
}
