using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Services.DTO.Category;
using BooksStore.Services.Implementation.Filters.CategoryFilters;
using BooksStore.Services.Interfaces.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using BooksStore.Services.Interfaces.Repositories;
using BooksStore.Common.Exceptions;

namespace BooksStore.Services.Implementation.Services.Base
{
    internal sealed class CategoryService : ICategoryService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IMapper _mapper;

        public CategoryService(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        public async Task AddCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = new Category(categoryDTO.Name);

            await _repositoryFactory.CreateCategoryRepository().AddAsync(category);
        }

        public async Task<CategoryDTO> GetCategoryById(int categoryId)
        {
            var category = await _repositoryFactory.CreateCategoryRepository().GetAsync(new CategoryByIdFilterSpec(categoryId));

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
            var category = await _repositoryFactory.CreateCategoryRepository().GetAsync(new CategoryByIdFilterSpec(categoryId));
            if (category == null)
                throw new NotFoundException(nameof(Category), category);

            await _repositoryFactory.CreateCategoryRepository().RemoveAsync(category);
        }

        public async Task UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _repositoryFactory.CreateCategoryRepository().UpdateAsync(category);
        }

        public async Task<int> GetCountCategories()
        {
            return await _repositoryFactory.CreateCategoryRepository().GetCountAsync();
        }
    }
}