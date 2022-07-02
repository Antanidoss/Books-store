using BooksStore.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.Interfaces.Repositories
{
    public interface IDiscountRepository
    {
        Task AddAsync(Discount discount);
        Task RemoveAsync(Discount discount);
        Task UpdateAsync(Discount discount);
        Task<IEnumerable<Discount>> GetAsync(int skip, int take);
        Task<Discount> GetByBookIdAsync(int bookId);
    }
}