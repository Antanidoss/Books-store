using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;

namespace BooksStore.Core.Entities
{
    public class Category : BaseEntity
    {
        [Index(Unique = true)]
        public string Name { get; set; }
        public IEnumerable<Book> Books { get; set; }

        public Category() : base()
        {
            Books = new List<Book>();
        }

        public Category(string name) : base()
        {
            ValidateArgumentConstructor(name);       

            Name = name;
            Books = new List<Book>();
        }

        private void ValidateArgumentConstructor(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Названия категории не может быть равна null либо пустой", nameof(name));
            }
        }
    }
}
