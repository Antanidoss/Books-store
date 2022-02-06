using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BooksStore.Core.Entities;
using BooksStore.Service.Interfaces.Filter;

namespace BooksStore.Service.Implementation.Filters.BookFilters.Specifications
{
    public class BookCategoryFilterSpecification : IFilterSpecification<Book>
    {
        private readonly IEnumerable<int> _categoriesId;

        public BookCategoryFilterSpecification(IEnumerable<int> categoriesId)
        {
            _categoriesId = categoriesId;
        }

        public Expression<Func<Book, bool>> GetSpecification()
        {
            return b => _categoriesId.ToList().Contains(b.CategoryId);
        }
    }
}
