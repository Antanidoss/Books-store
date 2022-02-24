using System;
using System.Linq.Expressions;
using BooksStore.Core.Entities;
using BooksStore.Services.Interfaces.Filter;

namespace BooksStore.Services.Implementation.Filters.BookFilters.Specifications
{
    public class BookPriceFilterSpecification : IFilterSpecification<Book>
    {
        private readonly decimal _booPriceFrom;

        private readonly decimal _booPriceTo;

        public BookPriceFilterSpecification(decimal booPriceTo, decimal booPriceFrom)
        {
            _booPriceTo = booPriceTo;
            _booPriceFrom = booPriceFrom;
        }

        public Expression<Func<Book, bool>> GetSpecification()
        {
            return b => b.Price >= _booPriceFrom && b.Price <= _booPriceTo;
        }
    }
}