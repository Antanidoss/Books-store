using BooksStore.Core.BasketModel;
using BooksStore.Core.BookModel;
using BooksStore.Core.Entities.Base;

namespace BooksStore.Core.BookBasketJunctionModel
{
    public class BookBasketJunction : BaseEntity
    {
        public Basket Basket { get; set; }
        public int BasketId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }

        public BookBasketJunction() : base() { }
    }
}
