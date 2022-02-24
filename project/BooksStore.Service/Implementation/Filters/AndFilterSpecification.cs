using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BooksStore.Core.Entities;
using BooksStore.Services.Interfaces.Filter;
using LinqKit;

namespace BooksStore.Services.Implementation.Filters
{
    public class AndFilterSpecification<T> : IFilterSpecification<T> where T : BaseEntity
    {
        private readonly List<IFilterSpecification<T>> _bookSpecifications;

        public AndFilterSpecification(IEnumerable<IFilterSpecification<T>> bookSpecifications)
        {
            _bookSpecifications = bookSpecifications.ToList();
        }

        public Expression<Func<T, bool>> GetSpecification()
        {
            var expression = _bookSpecifications.FirstOrDefault().GetSpecification();
            _bookSpecifications.RemoveAt(0);

            foreach (var specification in _bookSpecifications)
                expression.And(specification.GetSpecification());

            return expression;
        }
    }
}