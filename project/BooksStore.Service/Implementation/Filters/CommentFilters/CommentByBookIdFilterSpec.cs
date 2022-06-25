using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BooksStore.Services.Implementation.Filters.CommentFilters
{
    public sealed class CommentByBookIdFilterSpec : IQueryableFilterSpec<Comment>
    {
        private readonly int _bookId;

        public CommentByBookIdFilterSpec(int bookId)
        {
            _bookId = bookId;
        }

        public IQueryable<Comment> ApplyFilter(IQueryable<Comment> comments)
        {
            return comments.Where(ToExpression());
        }

        public Expression<Func<Comment, bool>> ToExpression()
        {
            return b => b.BookId == _bookId;
        }
    }
}
