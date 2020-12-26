using BooksStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<Category> GetCategoryByName(string categoryName);
        Task RemoveCategoryAsync(Category category);
        Task<IEnumerable<Category>> GetCategories(int skip, int take);
        Task UpdateCategoryAsync(Category category);
        Task<int> GetCountCategories();
    }
}
