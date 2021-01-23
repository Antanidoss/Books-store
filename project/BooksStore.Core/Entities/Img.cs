using System;

namespace BooksStore.Core.Entities
{
    public class Img : BaseEntity 
    {
        public string Path { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }

        public Img() : base() { }

        public Img(string path, int bookId) : base()
        {
            ValidateArgumentConstructor(path, bookId);

            Path = path;
            BookId = bookId;
        }

        private void ValidateArgumentConstructor(string path, int bookId)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Путь картинки не может быть пустой либо равен null", nameof(path));
            }
            if (bookId <= 0)
            {
                throw new ArgumentException("Id книги не может быть равен либо меньше 0", nameof(path));
            }
        }
    }
}
