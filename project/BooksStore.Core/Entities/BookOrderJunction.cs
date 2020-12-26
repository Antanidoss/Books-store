namespace BooksStore.Core.Entities
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
