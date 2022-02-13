using System;

namespace BooksStore.Core.Entities
{
    public class Img : BaseEntity
    {
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }

        private Img() : base() { }

        public Img(string name) : base()
        {
            ValidateArgumentConstructor(name);

            Name = name;
        }

        private void ValidateArgumentConstructor(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Image name cannot be empty", nameof(name));
            }
        }
    }
}