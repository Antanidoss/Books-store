using BooksStore.Web.Models.ViewModels.Book;
using BooksStore.Web.Models.ViewModels.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.ViewModels.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime TimeOfCreate { get; set; }
        public DateTime TimeOfDelivery { get; set; }
        public List<BookViewModel> BooksViewModel { get; set; }
        public bool OrderPlaced
        {
            get
            {
                if(DateTime.Now.Day >= TimeOfDelivery.Day && DateTime.Now.Month >= TimeOfDelivery.Month)
                {
                    return true;
                }
                return false;
            }
        }

    }
}
