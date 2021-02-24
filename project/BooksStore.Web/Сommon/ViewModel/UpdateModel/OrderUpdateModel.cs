using BooksStore.Web.Сommon.ViewModel.ReadModel;
using System.Collections.Generic;

namespace BooksStore.Web.Сommon.ViewModel.UpdateModel
{
    public class OrderUpdateModel
    {
        public int Id { get; set; }
        public List<BookViewModel> BooksViewModel { get; set; }
    }
}
