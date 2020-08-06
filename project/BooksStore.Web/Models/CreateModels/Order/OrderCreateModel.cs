using BooksStore.Web.Models.ViewModels.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.CreateModels.Order
{
    public class OrderCreateModel
    {
        public List<int> BookOrderIds { get; set; }
    }
}
