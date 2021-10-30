using BooksStore.Web.Сommon.ViewModel.ReadModel;
using System.Threading.Tasks;

namespace BooksStore.Web.Interfaces.Services
{
    public interface IBasketViewModelService
    {
        Task AddBasketBookAsync(int bookId);
        Task<BasketViewModel> GetBasketAsync(int pageNum);
        Task RemoveBasketBookAsync(int bookId);
        Task RemoveAllBasketBooksAsync();
    }
}
