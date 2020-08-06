using BooksStore.Core.CategoryModel;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.CategoryRep
{
    public class CategoryRepository : ICategoryRepository
    {
        EFDbContext context { get; set; }        
        public CategoryRepository(EFDbContext context) => this.context = context;

        public async Task AddCategoryAsync(Category category)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            var category = await context.Categories.FirstOrDefaultAsync(p => p.Id == categoryId);
            return category != default ? category : null;
        }

        public async Task RemoveCategoryAsync(Category category)
        {
            category = context.Categories
                .Include(p => p.Books)
                .FirstOrDefault(p => p.Id == category.Id);

            var otherCategory = context.Categories.FirstOrDefault(p => p.Name == "Разное");
            category.Books.AsParallel().ForAll((a) => a.CategoryId = otherCategory.Id);

            context.Books.UpdateRange(category.Books);

            context.Remove(category);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories(int skip, int take)
        {
            return await context.Categories
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            var category = await context.Categories.FirstOrDefaultAsync(p => p.Name == categoryName);
            return category != default ? category : null;
        }

        public async Task<int> GetCountCategories()
        {
            return await context.Categories.CountAsync();
        }
    }
}
