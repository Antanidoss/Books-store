using BooksStore.Core.Entities;

namespace BooksStore.Service.Interfaces.Filter
{
    public interface ISpecificationFilterBuilder<T> where T : BaseEntity
    {
        IFilterSpecification<T> GetResult(object filterModel);
    }
}