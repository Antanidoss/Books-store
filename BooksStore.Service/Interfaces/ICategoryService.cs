using BooksStore.Core.CategoryModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Service.Interfaces
{
    public interface ICategoryService
    {
        Task AddCategoryAsync(Category category);
        Task<Category> GetCategoryById(int categoryId);
        Task RemoveCategoryAsync(int categoryId);
        Task<IEnumerable<Category>> GetCategories(int skip, int take);
        Task UpdateCategoryAsync(Category category);
        Task<int> GetCountCategories();
    }
}
