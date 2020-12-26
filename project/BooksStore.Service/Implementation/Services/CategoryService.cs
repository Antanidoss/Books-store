using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO;
using BooksStore.Services.Interfaces;
using BooksStore.Web.CacheOptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;

        private readonly ICacheManager _cacheManager;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ICacheManager cacheManager)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }

        public async Task AddCategoryAsync(CategoryDTO categoryDTO)
        {
            if(categoryDTO == null)
            {
                throw new ArgumentNullException(nameof(CategoryDTO));
            }

            await _categoryRepository.AddCategoryAsync(_mapper.Map<Category>(categoryDTO));
        }
                              
        public async Task<CategoryDTO> GetCategoryById(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("id не может быть равен или меньше нуля");
            }

            return _mapper.Map<CategoryDTO>(await _categoryRepository.GetCategoryByIdAsync(categoryId));
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories(int skip, int take)
        {
            if (skip < 0 && take <= 0)
            {
                throw new ArgumentException("Некорректные аргументы skip и take");
            }
            return _mapper.Map<IEnumerable<CategoryDTO>>(await _categoryRepository.GetCategories(skip, take));
        }

        public async Task RemoveCategoryAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("id не может быть равен или меньше нуля");
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);

            if (category != default)
            {
                await _categoryRepository.RemoveCategoryAsync(category);
                _cacheManager.Remove(CacheKeys.GetCategoryKey(categoryId));
            }
        }

        public async Task UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            if (categoryDTO != null)
            {
                throw new ArgumentNullException(nameof(CategoryDTO));
            }

            await _categoryRepository.UpdateCategoryAsync(_mapper.Map<Category>(categoryDTO));
            _cacheManager.Remove(CacheKeys.GetCategoryKey(categoryDTO.Id));
        }

        public async Task<int> GetCountCategories()
        {
            return await _categoryRepository.GetCountCategories();
        }
    }
}
