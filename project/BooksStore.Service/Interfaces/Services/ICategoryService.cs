using BooksStore.Services.DTO.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.Interfaces
{
    public interface ICategoryService
    {
        Task AddCategoryAsync(CategoryDTO categoryDTO);
        Task<CategoryDTO> GetCategoryById(int categoryId);
        Task RemoveCategoryAsync(int categoryId);
        Task<IEnumerable<CategoryDTO>> GetCategories(int skip, int take);
        Task UpdateCategoryAsync(CategoryDTO categoryDTO);
        Task<int> GetCountCategories();
    }
}
