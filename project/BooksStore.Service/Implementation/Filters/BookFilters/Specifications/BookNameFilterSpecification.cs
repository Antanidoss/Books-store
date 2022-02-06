using System;
using System.Linq.Expressions;
using BooksStore.Core.Entities;
using BooksStore.Service.Interfaces.Filter;

namespace BooksStore.Service.Implementation.Filters.BookFilters.Specifications
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