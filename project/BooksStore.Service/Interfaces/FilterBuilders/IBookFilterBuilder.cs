using BooksStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BooksStore.Service.Interfaces.FilterBuilders
{
    public interface IBookFilterBuilder : IFilterBuilder<Book>
    {
        void BuildWithFilterName(string bookName);
        void BuildWithFilterCategories(List<int> categoryIds);
        void BuildWithFilterPrice(decimal bookPriceFrom, decimal bookPriceTo);
    }
}
