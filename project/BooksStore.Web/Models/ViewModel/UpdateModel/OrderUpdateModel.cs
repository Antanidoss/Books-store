using BooksStore.Web.Models.ViewModel.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.ViewModel.UpdateModel
{
    public class OrderUpdateModel
    {
        public int Id { get; set; }
        public List<BookViewModel> BooksViewModel { get; set; }
    }
}
