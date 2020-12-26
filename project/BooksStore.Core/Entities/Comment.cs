using BooksStore.Core.Entities;

namespace BooksStore.Core.Entities
{
    public class Comment : BaseEntity
    {
        public string Descriptions { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }

        public Comment() : base() { }
    }
}
