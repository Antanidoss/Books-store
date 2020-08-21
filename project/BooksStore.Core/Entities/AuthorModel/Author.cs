using BooksStore.Core.BookModel;
using BooksStore.Core.Entities.Base;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;

namespace BooksStore.Core.AuthorModel
{
    public class Author : BaseEntity
    {
        [Index(Unique = true)]
        public string Firstname { get; set; }
        [Index(Unique = true)]
        public string Surname { get; set; }
        public IEnumerable<Book> Books { get; set; }

        public Author() { }
        public override string ToString()
        {
            return Firstname + " " + Surname;
        }
    }
}
