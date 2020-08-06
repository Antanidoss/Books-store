using BooksStore.Core.CategoryModel;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Service.CategorySer
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository CategoryRepository { get; set; }
        public CategoryService(ICategoryRepository categoryRepository) => CategoryRepository = categoryRepository;

        public async Task AddCategoryAsync(Category category)
        {
            if(category != null && category != default)
            {
                await CategoryRepository.AddCategoryAsync(category);
            }
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            if (categoryId >= 1)
            {
                return await CategoryRepository.GetCategoryByIdAsync(categoryId);
            }
            return null;
        }

        public async Task<IEnumerable<Category>> GetCategories(int skip, int take)
        {
            if (skip >= 0 && take >= 1)
            {
                return await CategoryRepository.GetCategories(skip, take);
            }
            return new List<Category>();
        }

        public async Task RemoveCategoryAsync(int categoryId)
        {
            if (categoryId >= 1)
            {
                var category = await CategoryRepository.GetCategoryByIdAsync(categoryId);

                if (category != default)
                {
                    await CategoryRepository.RemoveCategoryAsync(category);
                }
            }
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            if (category != null && category != default)
            {
                await CategoryRepository.UpdateCategoryAsync(category);
            }
        }

        public async Task<int> GetCountCategories()
        {
            return await CategoryRepository.GetCountCategories();
        }
    }
}
