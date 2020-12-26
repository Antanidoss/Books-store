using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;

namespace BooksStore.Core.Entities
{
    public class Author : BaseEntity
    {
        [Index(Unique = true)]
        public string Firstname { get; set; }
        [Index(Unique = true)]
        public string Surname { get; set; }
        public IEnumerable<Book> Books { get; set; }

        public Author() : base()
        {
            Books = new List<Book>();
        }
        public override string ToString()
        {
            return Firstname + " " + Surname;
        }
    }
}
