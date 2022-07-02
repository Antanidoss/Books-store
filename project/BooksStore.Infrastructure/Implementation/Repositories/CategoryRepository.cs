using BooksStore.Core.Entities;
using BooksStore.Infastructure.Data;
using BooksStore.Services.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using QueryableFilterSpecification.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Implementation.Repositories
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly EFDbContext _context;
        public CategoryRepository(EFDbContext context) => _context = context;

        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Category category)
        {
            category = _context.Categories
                .Include(p => p.Books)
                .FirstOrDefault(p => p.Id == category.Id);

            var otherCategory = _context.Categories.FirstOrDefault(p => p.Name == "Разное");
            category.Books.AsParallel().ForAll((a) => a.CategoryId = otherCategory.Id);

            _context.Books.UpdateRange(category.Books);

            _context.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAsync(int skip, int take, IQueryableFilterSpec<Category> filter)
        {
            return await filter.ApplyFilter(_context.Categories.AsNoTracking())
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAsync(int skip, int take)
        {
            return await _context.Categories.AsNoTracking()
               .Skip(skip)
               .Take(take)
               .ToListAsync();
        }

        public async Task<Category> GetAsync(IQueryableFilterSpec<Category> filter)
        {
            return await _context.Categories.FirstOrDefaultAsync(filter.ToExpression());
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Categories.CountAsync();
        }
    }
}