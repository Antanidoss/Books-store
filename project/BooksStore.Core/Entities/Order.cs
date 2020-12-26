using System;
using System.Collections.Generic;

namespace BooksStore.Core.Entities
{
    public class Order : BaseEntity
    {
        public IEnumerable<BookOrderJunction> OrderBooks { get; set; }
        public DateTime TimeOfDelivery { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public Order() : base()
        {
            OrderBooks = new List<BookOrderJunction>();
            TimeOfDelivery = DateTime.Today.AddDays(3);
        }
    }
}
