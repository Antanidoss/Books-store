using QueryableFilterSpecification.Interfaces;
using System;
using BooksStore.Core.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace BooksStore.Services.Implementation.Filters.AuthorFilters
{
    public sealed class AuthorByFirstNameFilterSpec : IQueryableFilterSpec<Author>
    {
        private readonly string _firstName;

        public AuthorByFirstNameFilterSpec(string firstName)
        {
            _firstName = firstName;
        }

        public IQueryable<Author> ApplyFilter(IQueryable<Author> authors)
        {
            return authors.Where(ToExpression());
        }

        public Expression<Func<Author, bool>> ToExpression()
        {
            return a => a.Firstname == _firstName;
        }
    }
}
