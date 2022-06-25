using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BooksStore.Services.Implementation.Filters.CategoryFilters
{
    public sealed class CategoryByNameFilterSpec : IQueryableFilterSpec<Category>
    {
        private readonly string _name;

        public CategoryByNameFilterSpec(string name)
        {
            _name = name;
        }

        public IQueryable<Category> ApplyFilter(IQueryable<Category> categories)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<Category, bool>> ToExpression()
        {
            return c => c.Name == _name;
        }
    }
}
