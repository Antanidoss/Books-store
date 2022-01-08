using BooksStore.Core.Entities;
using System;
using System.Linq.Expressions;

namespace BooksStore.Service.Interfaces.FilterBuilders
{
    public interface IFilterBuilder<T> where T : BaseEntity
    {
        Expression<Func<T, bool>> GetResult();
    }
}