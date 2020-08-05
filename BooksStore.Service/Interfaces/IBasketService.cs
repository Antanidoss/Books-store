using BooksStore.Core.BasketModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Service.Interfaces
{
    public interface IBasketService
    {
        Task AddBasketAsync(Basket basket);
        Task AddBasketBookAsync(int basketId, int bookId);
        Task<Basket> GetBasketByIdAsync(int basketId);
        Task RemoveBasketAsync(int basketId);
        Task RemoveBasketBookAsync(int basketId, int bookId);
        Task RemoveAllBasketBooksAsync(int basketId);
        Task<IEnumerable<Basket>> GetBaskets(int skip, int take);
        Task UpdateBasketAsync(Basket basket);
    }
}
