using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System.Threading.Tasks;

namespace BooksStore.Services.Interfaces.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket> GetAsync(IQueryableFilterSpec<Basket> filter);
        Task UpdateAsync(Basket basket);
        Task<int> GetCountAsync(int basketId);
    }
}