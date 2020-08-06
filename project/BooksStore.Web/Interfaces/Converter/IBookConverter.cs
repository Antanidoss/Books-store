using BooksStore.Core.BookModel;
using BooksStore.Web.Models.ViewModels.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Interface.Converter
{
    public interface IBookConverter
    {
        BookViewModel ConvertToBookViewModel(Book book);
        IEnumerable<BookViewModel> ConvertToBookViewModel(IEnumerable<Book> books);

    }
}
