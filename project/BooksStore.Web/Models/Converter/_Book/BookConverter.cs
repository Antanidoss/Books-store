using BooksStore.Core.BookModel;
using BooksStore.Web.Interface.Converter;
using BooksStore.Web.Models.ViewModels.Book;
using BooksStore.Web.Models.ViewModels.Comment;
using BooksStore.Web.Models.ViewModels.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Converter._Book
{
    public class BookConverter : IBookConverter
    {
        public BookViewModel ConvertToBookViewModel(Book book)
        {
            if(book != null)
            {
                var bookViewModel = new BookViewModel()
                {
                    Id = book.Id,
                    Title = book.Title,
                    AuthorId = book.AuthorId,
                    AuthorName = book?.Author?.ToString() ?? string.Empty,
                    Price = book.Price,
                    CategoryId = book.CategoryId,
                    CategoryName = book?.Category?.Name ?? string.Empty,
                    Descriptions = book.Descriptions,
                    InStock = book.InStock,
                    NumberOfPages = book.NumberOfPages,
                    ImgPath = book?.Img?.Path ?? string.Empty,
                    
                };

                return bookViewModel;
            }
            return new BookViewModel();
        }

        public IEnumerable<BookViewModel> ConvertToBookViewModel(IEnumerable<Book> books)
        {
            if(books != null && books.Count() != 0)
            {
                List<BookViewModel> booksViewModel = new List<BookViewModel>();
                foreach(var book in books)
                {
                    booksViewModel.Add(ConvertToBookViewModel(book));
                }
                return booksViewModel;
            }
            return new List<BookViewModel>();
        }
    }
}
