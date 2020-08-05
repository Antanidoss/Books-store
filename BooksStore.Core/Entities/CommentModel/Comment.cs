using BooksStore.Core.AppUserModel;
using BooksStore.Core.BookModel;
using BooksStore.Core.Entities.Base;

namespace BooksStore.Core.CommentModel
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
