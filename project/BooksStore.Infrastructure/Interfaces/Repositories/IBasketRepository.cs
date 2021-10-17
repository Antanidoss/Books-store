using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces.Repositories;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces.Repositories
{
    public interface IBasketRepository : IRepository<Basket>
    {
        Task<int> GetCountAsync(int basketId);
    }
}