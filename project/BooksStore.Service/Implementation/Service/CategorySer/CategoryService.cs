using BooksStore.Infastructure.Interfaces;
using BooksStore.Service.Converter;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Service.CategorySer
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository CategoryRepository { get; set; }
        public CategoryService(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        public async Task AddCategoryAsync(CategoryDTO categoryDTO)
        {
            if(categoryDTO != null && categoryDTO != default)
            {
                await CategoryRepository.AddCategoryAsync(CategoryDTOConverter.ConvertToCategory(categoryDTO));
            }
        }
                              
        public async Task<CategoryDTO> GetCategoryById(int categoryId)
        {
            if (categoryId >= 1)
            {
                return CategoryDTOConverter.ConvertToCategoryDTO(await CategoryRepository.GetCategoryByIdAsync(categoryId));
            }
            return null;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories(int skip, int take)
        {
            if (skip >= 0 && take >= 1)
            {
                return CategoryDTOConverter.ConvertToCategoryDTO(await CategoryRepository.GetCategories(skip, take));
            }
            return new List<CategoryDTO>();
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

        public async Task UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            if (categoryDTO != null && categoryDTO != default)
            {
                await CategoryRepository.UpdateCategoryAsync(CategoryDTOConverter.ConvertToCategory(categoryDTO));
            }
        }

        public async Task<int> GetCountCategories()
        {
            return await CategoryRepository.GetCountCategories();
        }
    }
}
