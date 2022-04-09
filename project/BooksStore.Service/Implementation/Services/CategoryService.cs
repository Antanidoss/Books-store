using AutoMapper;
using BooksStore.Common.Exceptions;
using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO.Category;
using BooksStore.Services.Interfaces;
using BooksStore.Web.CacheOptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services
{
    internal sealed class CategoryService : ICategoryService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IMapper _mapper;

        private readonly ICacheManager _cacheManager;

        public CategoryService(IRepositoryFactory repositoryFactory, IMapper mapper, ICacheManager cacheManager)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }

        public async Task AddCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = new Category(categoryDTO.Name);

            await _repositoryFactory.CreateCategoryRepository().AddAsync(category);
        }

        public async Task<CategoryDTO> GetCategoryById(int categoryId)
        {
            var category = await _repositoryFactory.CreateCategoryRepository().GetByIdAsync(categoryId);

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), category);
            }

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories(int skip, int take)
        {
            var categories = await _repositoryFactory.CreateCategoryRepository().GetAsync(skip, take);

            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task RemoveCategoryAsync(int categoryId)
        {
            var category = await _repositoryFactory.CreateCategoryRepository().GetByIdAsync(categoryId);

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), category);
            }

            await _repositoryFactory.CreateCategoryRepository().RemoveAsync(category);
            _cacheManager.Remove(CacheKeys.GetCategoryKey(categoryId));
        }

        public async Task UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _repositoryFactory.CreateCategoryRepository().UpdateAsync(category);
            _cacheManager.Remove(CacheKeys.GetCategoryKey(categoryDTO.Id));
        }

        public async Task<int> GetCountCategories()
        {
            return await _repositoryFactory.CreateCategoryRepository().GetCount();
        }
    }
}