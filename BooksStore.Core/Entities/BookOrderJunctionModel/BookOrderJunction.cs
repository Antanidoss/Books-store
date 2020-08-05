using BooksStore.Core.BookModel;
using BooksStore.Core.Entities.Base;
using BooksStore.Core.OrderModel;

namespace BooksStore.Core.BookOrderJunctionModel
{
    public class BookOrderJunction : BaseEntity
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }

        public BookOrderJunction() : base() { }
    }
}
