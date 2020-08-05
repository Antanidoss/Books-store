using BooksStore.Web.Models.ViewModels.Book;
using BooksStore.Web.Models.ViewModels.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.ViewModels.Basket
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
