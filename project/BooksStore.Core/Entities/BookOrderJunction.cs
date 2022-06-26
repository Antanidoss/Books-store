using System;

namespace BooksStore.Core.Entities
{
    public class BookOrderJunction : BaseEntity
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }

        private BookOrderJunction() : base() { }

        public BookOrderJunction(int orderId, int bookId) : base()
        {
            ValidateArgumentConstructor(orderId, bookId);

            OrderId = orderId;
            BookId = bookId;
        }

        private void ValidateArgumentConstructor(int orderId, int bookId)
        {
            if (orderId <= 0)
                throw new ArgumentException("Id заказа не может быть равен либо меньше 0", nameof(orderId));

            if (bookId <= 0)
                throw new ArgumentException("Id книги не может быть равен либо меньше 0", nameof(bookId));
        }
    }
}