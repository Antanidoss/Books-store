namespace BooksStore.Core.Entities
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
