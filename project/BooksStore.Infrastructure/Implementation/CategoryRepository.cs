using BooksStore.Core.Entities;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Infastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EFDbContext _context;      
        public CategoryRepository(EFDbContext context) => this._context = context;

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == categoryId);
            return category != default ? category : null;
        }

        public async Task RemoveCategoryAsync(Category category)
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

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories(int skip, int take)
        {
            return await _context.Categories
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(p => p.Name == categoryName);
            return category != default ? category : null;
        }

        public async Task<int> GetCountCategories()
        {
            return await _context.Categories.CountAsync();
        }
    }
}
