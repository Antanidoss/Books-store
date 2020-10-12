﻿using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.Managers
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryService _categoryService;

        private readonly IMapper _mapper;
        public CategoryManager(ICategoryService categoryService, IMapper mapper)
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
            var pageSize = PageSizes.Categories;
            return _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryService.GetCategories((pageNum - 1) * pageSize, pageSize));
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
