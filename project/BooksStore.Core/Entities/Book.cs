using System.Collections.Generic;

namespace BooksStore.Core.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPages { get; set; }
        public bool InStock { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<BookOrderJunction> BookOrders { get; set; }
        public IEnumerable<BookBasketJunction> BookBaskets { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public Img Img { get; set; }

        public Book() : base()
        {
            Comments = new List<Comment>();
            BookOrders = new List<BookOrderJunction>();
            BookBaskets = new List<BookBasketJunction>();
        }        
    }
}
