using BooksStore.Core.Entities;
using BooksStore.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Services.Interfaces
{
    public interface IBasketService
    {
        Task AddBasketBookAsync(int basketId, int bookId);
        Task<BasketDTO> GetBasketByIdAsync(int basketId);
        Task RemoveBasketBookAsync(int basketId, int bookId);
        Task RemoveAllBasketBooksAsync(int basketId);
        Task<int> GetBasketBookCount(int basketId);
    }
}
