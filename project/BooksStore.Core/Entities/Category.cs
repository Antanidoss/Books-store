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
    }
}
