using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BooksStore.Services.Implementation.Filters.OrderFilters
{
    public sealed class OrderByIdFilterSpec : IQueryableFilterSpec<Order>
    {
        private readonly int _orderId;

        public OrderByIdFilterSpec(int orderId)
        {
            _orderId = orderId;
        }

        public IQueryable<Order> ApplyFilter(IQueryable<Order> orders)
        {
            return orders.Where(ToExpression());
        }

        public Expression<Func<Order, bool>> ToExpression()
        {
            return o => o.Id == _orderId;
        }
    }
}
