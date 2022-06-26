using BooksStore.Core.Entities;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using QueryableFilterSpecification.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Implementation.Repositories
{
    internal class BasketRepository : IBasketRepository
    {
        private readonly EFDbContext _context;

        public BasketRepository(EFDbContext context) => _context = context;

        public async Task UpdateAsync(Basket basket)
        {
            _context.Baskets.Update(basket);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync(int basketId)
        {
            var basket = await _context.Baskets
                .Include(b => b.BasketBooks)
                .FirstOrDefaultAsync(b => b.Id == basketId);

            return basket == null ? default : basket.BasketBooks.Count();
        }

        public async Task<Basket> GetAsync(IQueryableFilterSpec<Basket> filter)
        {
            return await _context.Baskets
                .Include(p => p.BasketBooks)
                .ThenInclude(p => p.Book)
                .ThenInclude(p => p.Img)
                .Include(p => p.BasketBooks)
                .ThenInclude(p => p.Book.Author)
                .FirstOrDefaultAsync(filter.ToExpression());
        }
    }
}