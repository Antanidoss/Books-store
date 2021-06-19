using BooksStore.Core.Entities;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Implementation.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EFDbContext _context;      
        public CategoryRepository(EFDbContext context) => _context = context;

        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetByIdAsync(int categoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == categoryId);
            return category != default ? category : null;
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

        public async Task<IEnumerable<Category>> GetAsync(int skip, int take)
        {
            return await _context.Categories
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<Category> GetByNameAsync(string categoryName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(p => p.Name == categoryName);
            return category != default ? category : null;
        }

        public async Task<int> GetCount()
        {
            return await _context.Categories.CountAsync();
        }
    }
}
