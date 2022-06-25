using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BooksStore.Services.Implementation.Filters.CommentFilters
{
    public sealed class CommentByIdFilterSpec : IQueryableFilterSpec<Comment>
    {
        private readonly int _commentId;

        public CommentByIdFilterSpec(int commentId)
        {
            _commentId = commentId;
        }

        public IQueryable<Comment> ApplyFilter(IQueryable<Comment> comments)
        {
            return comments.Where(ToExpression());
        }

        public Expression<Func<Comment, bool>> ToExpression()
        {
            return c => c.Id == _commentId;
        }
    }
}
