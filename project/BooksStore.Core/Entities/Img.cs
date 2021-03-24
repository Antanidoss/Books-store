using System;

namespace BooksStore.Core.Entities
{
    public class Img : BaseEntity 
    {
        public string Path { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }

        private Img() : base() { }

        public Img(string path) : base()
        {
            ValidateArgumentConstructor(path);

            Path = path;
        }

        private void ValidateArgumentConstructor(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Путь картинки не может быть пустой либо равен null", nameof(path));
            }            
        }
    }
}
