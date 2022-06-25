using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BooksStore.Services.Implementation.Filters.BasketFilters
{
    public sealed class BasketByIdFilterSpec : IQueryableFilterSpec<Basket>
    {
        private readonly int _basketId;

        public BasketByIdFilterSpec(int basketId)
        {
            _basketId = basketId;
        }

        public IQueryable<Basket> ApplyFilter(IQueryable<Basket> baskets)
        {
            return baskets.Where(ToExpression());
        }

        public Expression<Func<Basket, bool>> ToExpression()
        {
            return b => b.Id == _basketId;
        }
    }
}
