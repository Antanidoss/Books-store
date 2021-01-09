using BooksStore.Core.Entities;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Infastructure
{
    public class BasketRepository : IBasketRepository
    {
        private readonly EFDbContext _context;

        public BasketRepository(EFDbContext context) => this._context = context;

        public async Task AddBasketAsync(Basket basket)
        {
            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();
        }

        public async Task<Basket> GetBasketById(int basketId)
        {
            var basket = await _context.Baskets
                .Include(p => p.BasketBooks)
                .ThenInclude(p => p.Book)
                .ThenInclude(p => p.Img)
                .FirstOrDefaultAsync(p => p.Id == basketId);       
            
            return basket != default ? basket : null;
        }

        public async Task RemoveBasketAsync(Basket basket)
        {
            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBasketAsync(Basket basket)
        {
            _context.Baskets.Update(basket);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Basket>> GetBaskets(int skip, int take)
        {
            return await _context.Baskets
                .Skip(skip)
                .Take(take)
                .Include(p => p.BasketBooks)
                .ThenInclude(p => p.Book)
                .ToListAsync();
        }

        public async Task<int> GetBasketBookCount(int basketId)
        {
            return (await _context.Baskets.Include(b => b.BasketBooks)
                .FirstOrDefaultAsync(b => b.Id == basketId))
                .BasketBooks.Count();
        }
    }
}
