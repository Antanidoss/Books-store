using System;
using System.Collections.Generic;

namespace BooksStore.Core.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPages { get; set; }
        public bool InStock { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<BookOrderJunction> BookOrders { get; set; }
        public IEnumerable<BookBasketJunction> BookBaskets { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public Img Img { get; set; }

        private Book() : base()
        {
            Comments = new List<Comment>();
            BookOrders = new List<BookOrderJunction>();
            BookBaskets = new List<BookBasketJunction>();
        }

        public Book(string title, decimal price, bool inStock, int numberOfPages, string descriptions, Img img,
            Author author, Category category) : base()
        {
            ValidateArgumentConstructor(title, price, inStock, numberOfPages, descriptions, img, author, category);

            Title = title;
            Price = price;
            InStock = inStock;
            NumberOfPages = numberOfPages;
            Descriptions = descriptions;
            Img = img;
            Author = author;
            Category = category;
        }

        private void ValidateArgumentConstructor(string title, decimal price, bool inStock, int numberOfPages, string descriptions, Img img, Author author, Category category)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Названия книги не может быть пустой либо равным null", nameof(title));

            if (price < 0)
                throw new ArgumentException("Цена не может быть меньше 0", nameof(price));

            if (numberOfPages <= 0)
                throw new ArgumentException("Количество страниц не может быть меньше либо равным 0", nameof(numberOfPages));

            if (string.IsNullOrEmpty(descriptions))
                throw new ArgumentException("Описания книги не может быть пустым либо равным null", nameof(descriptions));

            if (img == null)
                throw new ArgumentException("Картинка книги не может быть равным null", nameof(img));

            if (author == null)
                throw new ArgumentException("Автор книги книги не может быть равным null", nameof(author));

            if (category == null)
                throw new ArgumentException("Категория книги книги не может быть равным null", nameof(author));
        }
    }
}