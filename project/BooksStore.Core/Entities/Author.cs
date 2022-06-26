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

        private Author() : base()
        {
            Books = new List<Book>();
        }

        public Author(string firstname, string surname)
        {
            ValidateArgumentConstructor(firstname, surname);

            Firstname = firstname;
            Surname = surname;
        }

        private void ValidateArgumentConstructor(string firstname, string surname)
        {
            if (string.IsNullOrEmpty(firstname))
                throw new ArgumentException("Имя автора не может быть пустой либо равен null", nameof(firstname));

            if (string.IsNullOrEmpty(surname))
                throw new ArgumentException("Фамилия автора не может быть пустой либо равен null", nameof(surname));
        }

        public override string ToString()
        {
            return Firstname + " " + Surname;
        }
    }
}