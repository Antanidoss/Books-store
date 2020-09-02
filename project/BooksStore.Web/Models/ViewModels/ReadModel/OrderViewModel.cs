using System;
using System.Collections.Generic;

namespace BooksStore.Web.Models.ViewModels
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
