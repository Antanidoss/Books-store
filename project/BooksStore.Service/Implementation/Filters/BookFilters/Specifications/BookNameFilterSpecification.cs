using System;
using System.Linq.Expressions;
using BooksStore.Core.Entities;
using BooksStore.Services.Interfaces.Filter;

namespace BooksStore.Services.Implementation.Filters.BookFilters.Specifications
{
    public class BookNameFilterSpecification : IFilterSpecification<Book>
    {
        private readonly string _bookName;

        public BookNameFilterSpecification(string bookName)
        {
            _bookName = bookName.ToLower().Replace(oldValue: " ", newValue: "");
        }

        public Expression<Func<Book, bool>> GetSpecification()
        {
            return b => b.Title.ToLower().Replace(" ", "").Contains(_bookName);
        }
    }
}