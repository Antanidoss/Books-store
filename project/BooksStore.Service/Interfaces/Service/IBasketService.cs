using BooksStore.Core.BasketModel;
using BooksStore.Service.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Service.Interfaces
{
    public interface IBasketService
    {
        Task AddBasketAsync(BasketDTO basketDTO);
        Task AddBasketBookAsync(int basketId, int bookId);
        Task<BasketDTO> GetBasketByIdAsync(int basketId);
        Task RemoveBasketAsync(int basketId);
        Task RemoveBasketBookAsync(int basketId, int bookId);
        Task RemoveAllBasketBooksAsync(int basketId);
        Task<IEnumerable<BasketDTO>> GetBaskets(int skip, int take);
        Task UpdateBasketAsync(BasketDTO basketDTO);
    }
}
