using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BooksStore.Services.Implementation.Filters.AuthorFilters
{
    internal class AuthorByIdFilterSpec : IQueryableFilterSpec<Author>
    {
        private readonly int _authorId;

        public AuthorByIdFilterSpec(int authorId)
        {
            _authorId = authorId;
        }

        public IQueryable<Author> ApplyFilter(IQueryable<Author> authors)
        {
            return authors.Where(ToExpression());
        }

        public Expression<Func<Author, bool>> ToExpression()
        {
            return a => a.Id == _authorId;
        }
    }
}
