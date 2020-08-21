using BooksStore.Web.Models.ViewModels.Comment;
using BooksStore.Web.Models.ViewModels.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.ViewModels.Book
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPages { get; set; }
        public bool InStock { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string AuthorFullName { get; set; }
        public int AuthorId { get; set; }
        public string ImgPath { get; set; }
        public bool IsAddToBasket { get; set; }
    }
}
