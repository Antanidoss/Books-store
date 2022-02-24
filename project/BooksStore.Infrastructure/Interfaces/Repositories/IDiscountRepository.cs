using BooksStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Infrastructure.Interfaces.Repositories
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<IEnumerable<Discount>> GetAsync(int skip, int take);
        Task<Discount> GetByBookIdAsync(int bookId);
    }
}