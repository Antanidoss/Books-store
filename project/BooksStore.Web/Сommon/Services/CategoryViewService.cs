using AutoMapper;
using BooksStore.Services.DTO.Category;
using BooksStore.Web.Interfaces.Services;
using BooksStore.Web.Сommon.Pagination;
using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using BooksStore.Web.Сommon.ViewModel.UpdateModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BooksStore.Common.Constants;
using BooksStore.Services.Interfaces.Services.Base;

namespace BooksStore.Web.Сommon.Services
{
    public class CategoryViewService : ICategoryViewModelService
    {
        private readonly ICategoryService _categoryService;

        private readonly IMapper _mapper;
        public CategoryViewService(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task AddCategoryAsync(CategoryCreateModel model)
        {
            await _categoryService.AddCategoryAsync(_mapper.Map<CategoryDTO>(model));
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategories(int pageNum)
        {
            var take = PageSizes.Categories;
            var skip = PaginationInfo.GetCountSkipItems(pageNum, take);
            var categories = await _categoryService.GetCategories(skip, take);

            return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategories(int skip, int take)
        {
            var categories = await _categoryService.GetCategories(skip, take);

            return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public async Task<CategoryViewModel> GetCategoryById(int categoryId)
        {
            return _mapper.Map<CategoryViewModel>(await _categoryService.GetCategoryById(categoryId));
        }

        public async Task<int> GetCountCategories()
        {
            return await _categoryService.GetCountCategories();
        }

        public async Task RemoveCategoryAsync(int categoryId)
        {
            await _categoryService.RemoveCategoryAsync(categoryId);
        }

        public async Task UpdateCategoryAsync(CategoryUpdateModel model)
        {
            await _categoryService.UpdateCategoryAsync(_mapper.Map<CategoryDTO>(model));
        }
    }
}
