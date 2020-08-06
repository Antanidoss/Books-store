using BooksStore.Core.AppUserModel;
using BooksStore.Core.BookBasketJunctionModel;
using BooksStore.Core.Entities.Base;
using System.Collections.Generic;

namespace BooksStore.Core.BasketModel
{
    public class Basket : BaseEntity
    {
        public IEnumerable<BookBasketJunction> BookBaskets { get; set; }
        public AppUser AppUser { get; set; }

        public Basket() : base()
        {
            BookBaskets = new List<BookBasketJunction>();
        }
    }
}
