using System;
using System.Linq.Expressions;
using BooksStore.Core.Entities;

namespace BooksStore.Services.Interfaces.Filter
{
    public interface IFilterSpecification<T> where T : BaseEntity
    {
        Expression<Func<T, bool>> GetSpecification();
    }
}