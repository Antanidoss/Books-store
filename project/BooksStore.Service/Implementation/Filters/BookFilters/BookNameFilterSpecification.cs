using System;
using System.Linq;
using System.Linq.Expressions;
using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;

namespace BooksStore.Services.Implementation.Filters.BookFilters
{
    public class BookNameFilterSpecification : IQueryableFilterSpec<Book>
    {
        private readonly string _bookName;

        public BookNameFilterSpecification(string bookName)
        {
            _bookName = bookName.ToLower().Replace(" ", string.Empty);
        }

        public IQueryable<Book> ApplyFilter(IQueryable<Book> books)
        {
            return books.Where(ToExpression());
        }

        public Expression<Func<Book, bool>> ToExpression()
        {
            return b => b.Title.ToLower().Replace(" ", string.Empty).Contains(_bookName);
        }
    }
}