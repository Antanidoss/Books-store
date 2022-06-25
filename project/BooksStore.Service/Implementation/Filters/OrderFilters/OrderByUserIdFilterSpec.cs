using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BooksStore.Services.Implementation.Filters.OrderFilters
{
    public sealed class OrderByUserIdFilterSpec : IQueryableFilterSpec<Order>
    {
        private readonly string _userId;

        public OrderByUserIdFilterSpec(string userId)
        {
            _userId = userId;
        }

        public IQueryable<Order> ApplyFilter(IQueryable<Order> orders)
        {
            return orders.Where(ToExpression());
        }

        public Expression<Func<Order, bool>> ToExpression()
        {
            return o => o.AppUserId == _userId;
        }
    }
}
