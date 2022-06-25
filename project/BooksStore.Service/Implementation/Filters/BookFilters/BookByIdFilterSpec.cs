using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BooksStore.Services.Implementation.Filters.BookFilters
{
    public class BookByIdFilterSpec : IQueryableFilterSpec<Book>
    {
        private readonly int _bookId;

        public BookByIdFilterSpec(int bookId)
        {
            _bookId = bookId;
        }

        public IQueryable<Book> ApplyFilter(IQueryable<Book> books)
        {
            return books.Where(ToExpression());
        }

        public Expression<Func<Book, bool>> ToExpression()
        {
            return b => b.Id == _bookId;
        }
    }
}
