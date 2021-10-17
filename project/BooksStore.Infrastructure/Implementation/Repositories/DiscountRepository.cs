using BooksStore.Core.Entities;
using BooksStore.Infastructure.Data;
using BooksStore.Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Infrastructure.Implementation.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private EFDbContext _context;
        public DiscountRepository(EFDbContext context) => _context = context;

        public async Task AddAsync(Discount entity)
        {
            _context.Discounts.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Discount>> GetAsync(int skip, int take)
        {
            return await _context.Discounts
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<Discount> GetByIdAsync(int id)
        {
            var discount = await _context.Discounts.FirstOrDefaultAsync(p => p.Id == id);

            return discount != default ? discount : null;
        }

        public async Task<Discount> GetByBookIdAsync(int bookId)
        {
            var discount = await _context.Discounts.Include(p => p.BookId).FirstOrDefaultAsync(p => p.BookId == bookId);

            return discount != default ? discount : null;
        }

        public async Task RemoveAsync(Discount entity)
        {
            _context.Discounts.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Discount entity)
        {
            _context.Discounts.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}