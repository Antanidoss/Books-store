using System.Collections.Generic;

namespace BooksStore.Core.Entities
{
    public class Basket : BaseEntity
    {
        public IEnumerable<BookBasketJunction> BasketBooks { get; set; }
        public AppUser AppUser { get; set; }

        public Basket() : base()
        {
            BasketBooks = new List<BookBasketJunction>();
        }
    }
}
