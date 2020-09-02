using BooksStore.Web.Models.ViewModel.Index;
using System.Linq;

namespace BooksStore.Web.Models.ViewModel.ReadModel
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public IndexViewModel<BookViewModel> BookIndexModel { get; set; }
        public decimal TotalPrice
        {
            get { return BookIndexModel.Objects.Sum(p => p.Price); }
        }

        public BasketViewModel()
        {
            BookIndexModel = new IndexViewModel<BookViewModel>();
        }

    }
}
