using BooksStore.Core.BookModel;
using BooksStore.Core.Entities.Base;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;

namespace BooksStore.Core.CategoryModel
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
