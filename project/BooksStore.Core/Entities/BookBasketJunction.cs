using System;

namespace BooksStore.Core.Entities
{
    public class BookBasketJunction : BaseEntity
    {
        public Basket Basket { get; set; }
        public int BasketId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }

        private BookBasketJunction() : base() { }

        public BookBasketJunction(int basketId, int bookId) : base()
        {
            ValidateArgumentConstructor(basketId, bookId);

            BasketId = basketId;
            BookId = bookId;
        }

        private void ValidateArgumentConstructor(int basketId, int bookId)
        {
            if (basketId <= 0)
            {
                throw new ArgumentException("Id корзина не может быть равен либо меньше 0", nameof(basketId));
            }
            if (bookId <= 0)
            {
                throw new ArgumentException("Id книги не может быть равен либо меньше 0", nameof(bookId));
            }
        }
    }
}
