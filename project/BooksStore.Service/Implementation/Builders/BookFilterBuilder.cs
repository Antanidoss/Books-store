using BooksStore.Core.Entities;
using BooksStore.Service.Interfaces.FilterBuilders;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BooksStore.Service.Implementation.Builders
{
    class BookFilterBuilder : IBookFilterBuilder
    {
        private Expression<Func<Book, bool>> result;
        public void BuildWithFilterCategories(List<int> categoryIds)
        {
            Expression<Func<Book, bool>> categoryFilter = b => categoryIds.Contains(b.CategoryId);

            if (result == null)
            {
                result = categoryFilter;
            }
            else
            {
                result = result.And(categoryFilter);
            }
        }

        public void BuildWithFilterName(string bookName)
        {
            Expression<Func<Book, bool>> nameFilter = b => b.Title.ToLower().Replace(" ", "").Contains(bookName);

            if (result == null)
            {
                result = nameFilter;
            }
            else
            {
                result = result.And(nameFilter);
            }
        }

        public void BuildWithFilterPrice(decimal bookPriceFrom, decimal bookPriceTo)
        {
            Expression<Func<Book, bool>> priceFilter = b => b.Price >= bookPriceFrom && b.Price <= bookPriceTo;

            if (result == null)
            {
                result = priceFilter;
            }
            else
            {
                result = result.And(priceFilter);
            }
        }

        public Expression<Func<Book, bool>> GetResult()
        {
            return result;
        }
    }
}