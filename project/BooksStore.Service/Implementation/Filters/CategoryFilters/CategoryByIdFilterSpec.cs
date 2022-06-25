using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BooksStore.Services.Implementation.Filters.CategoryFilters
{
    public sealed class CategoryByIdFilterSpec : IQueryableFilterSpec<Category>
    {
        private readonly int _categoryId;

        public CategoryByIdFilterSpec(int categoryId)
        {
            _categoryId = categoryId;
        }

        public IQueryable<Category> ApplyFilter(IQueryable<Category> categories)
        {
            return categories.Where(ToExpression());
        }

        public Expression<Func<Category, bool>> ToExpression()
        {
            return c => c.Id == _categoryId;
        }
    }
}
