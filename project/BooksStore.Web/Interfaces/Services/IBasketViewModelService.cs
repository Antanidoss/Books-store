using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Interfaces.Managers
{
    public interface IBasketViewModelService
    {
        Task AddBasketBookAsync(int bookId);
        Task<BasketViewModel> GetBasketAsync(int pageNum);
        Task RemoveBasketBookAsync(int bookId);
        Task RemoveAllBasketBooksAsync();
    }
}
