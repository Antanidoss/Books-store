using System;

namespace BooksStore.Core.Entities
{
    public class Comment : BaseEntity
    {
        public string Descriptions { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }

        private Comment() : base() { }

        public Comment(string descriptions, int bookId, string appUserId) : base()
        {
            ValidateArgumentConstructor(descriptions, bookId, appUserId);

            Descriptions = descriptions;
            BookId = bookId;
            AppUserId = appUserId;
        }

        private void ValidateArgumentConstructor(string descriptions, int bookId, string appUserId)
        {
            if (string.IsNullOrEmpty(descriptions))
                throw new ArgumentException("Описания коментария не может быть пустой либо равен null", nameof(descriptions));

            if (bookId <= 0)
                throw new ArgumentException("Id книги не может быть равен либо меньше 0", nameof(bookId));

            if (string.IsNullOrEmpty(appUserId))
                throw new ArgumentException("Id пользователя не может быть пустой либо равен null", nameof(appUserId));
        }
    }
}