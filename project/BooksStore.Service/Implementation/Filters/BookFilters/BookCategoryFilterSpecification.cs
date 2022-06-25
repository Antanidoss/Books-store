using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;

namespace BooksStore.Services.Implementation.Filters.BookFilters
{
    public class BookCategoryFilterSpecification : IQueryableFilterSpec<Book>
    {
        private readonly IEnumerable<int> _categoriesId;

        public BookCategoryFilterSpecification(IEnumerable<int> categoriesId)
        {
            _categoriesId = categoriesId;
        }

        public IQueryable<Book> ApplyFilter(IQueryable<Book> books)
        {
            return books.Where(ToExpression());
        }

        public Expression<Func<Book, bool>> ToExpression()
        {
            return b => _categoriesId.ToList().Contains(b.CategoryId);
        }
    }
}
