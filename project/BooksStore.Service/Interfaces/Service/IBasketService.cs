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
        Task AddBasketBookAsync(int basketId, int bookId);
        Task<BasketDTO> GetBasketByIdAsync(int basketId);
        Task RemoveBasketBookAsync(int basketId, int bookId);
        Task RemoveAllBasketBooksAsync(int basketId);
    }
}
