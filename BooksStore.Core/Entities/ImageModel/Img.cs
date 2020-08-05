using BooksStore.Core.BookModel;
using BooksStore.Core.Entities.Base;

namespace BooksStore.Core.ImageModel
{
    public class Img : BaseEntity 
    {
        public string Path { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }

        public Img() : base() { }
    }
}
