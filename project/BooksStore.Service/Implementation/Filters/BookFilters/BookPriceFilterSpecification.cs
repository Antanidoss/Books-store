using System;
using System.Linq;
using System.Linq.Expressions;
using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;

namespace BooksStore.Services.Implementation.Filters.BookFilters
{
    public class BookPriceFilterSpecification : IQueryableFilterSpec<Book>
    {
        private readonly decimal _booPriceFrom;

        private readonly decimal _booPriceTo;

        public BookPriceFilterSpecification(decimal booPriceFrom, decimal booPriceTo)
        {
            _booPriceTo = booPriceTo;
            _booPriceFrom = booPriceFrom;
        }

        public IQueryable<Book> ApplyFilter(IQueryable<Book> books)
        {
            return books.Where(ToExpression());
        }

        public Expression<Func<Book, bool>> ToExpression()
        {
            return b => b.Price >= _booPriceFrom && b.Price <= _booPriceTo;
        }
    }
}