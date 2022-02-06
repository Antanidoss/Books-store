using System;
using System.Collections.Generic;
using System.Linq;
using BooksStore.Core.Entities;
using BooksStore.Service.Implementation.Filters.BookFilters.Specifications;
using BooksStore.Service.Interfaces.Filter;
using BooksStore.Service.Models;

namespace BooksStore.Service.Implementation.Filters.BookFilters
{
    public class BookFilterBuilder : ISpecificationFilterBuilder<Book>
    {
        public IFilterSpecification<Book> GetResult(object filterModel)
        {
            var concreteFilterModel = new BookFilterModel();

            if ((concreteFilterModel = filterModel as BookFilterModel) == null)
                throw new ArgumentException("Invalid filter model");

            var specifications = new List<IFilterSpecification<Book>>();

            if (!string.IsNullOrEmpty(concreteFilterModel.BookName))
                specifications.Add(new BookNameFilterSpecification(concreteFilterModel.BookName));

            if (concreteFilterModel.CategoryIds.Any())
                specifications.Add(new BookCategoryFilterSpecification(concreteFilterModel.CategoryIds));

            specifications.Add(new BookPriceFilterSpecification(concreteFilterModel.BookPriceTo, concreteFilterModel.BookPriceFrom));

            return new AndFilterSpecification<Book>(specifications);
        }
    }
}