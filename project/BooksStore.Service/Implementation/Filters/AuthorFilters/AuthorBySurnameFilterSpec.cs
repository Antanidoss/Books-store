using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BooksStore.Services.Implementation.Filters.AuthorFilters
{
    public sealed class AuthorBySurnameFilterSpec : IQueryableFilterSpec<Author>
    {
        private readonly string _surname;

        public AuthorBySurnameFilterSpec(string surname)
        {
            _surname = surname;
        }

        public IQueryable<Author> ApplyFilter(IQueryable<Author> authors)
        {
            return authors.Where(ToExpression());
        }

        public Expression<Func<Author, bool>> ToExpression()
        {
            return a => a.Surname == _surname;
        }
    }
}
