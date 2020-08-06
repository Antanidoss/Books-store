using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Core.CategoryModel;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Cache;
using BooksStore.Web.Interface.Converter;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModels.Category;
using BooksStore.Web.Models.ViewModels.Index;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BooksStore.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        ICategoryService CategoryService { get; set; }
        ICategoryConverter CategoryConverter { get; set; }
        public CategoryController(ICategoryService categoryService, ICategoryConverter categoryConverter)
        {
            CategoryService = categoryService;
            CategoryConverter = categoryConverter;
        }


        [HttpGet]
        public IActionResult AddCategory() => View();
        [HttpPost]
        public async Task<IActionResult> AddCategory([Required(ErrorMessage ="Введите название категории")]string categoryName)
        {
            if (ModelState.IsValid)
            {
                await CategoryService.AddCategoryAsync(new Category() { Name = categoryName });
                return RedirectToAction("IndexСategoriesAdmin", "Category");
            }
            return View(categoryName);
        }


        [HttpGet]
        public async Task<IActionResult> IndexСategoriesAdmin(int pageNum = 1)
        {
            return await IndexCategories(pageNum);
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> IndexCategories(int pageNum = 1)
        {
            if (pageNum >= 1)
            {
                int pageSize = 6;
             
                var categories = (await CategoryService.GetCategories((pageNum - 1) * pageSize, pageSize)).ToList();
                
                IndexViewModel<CategoryViewModel> categoryIndexModel = new IndexViewModel<CategoryViewModel>(pageNum, pageSize,
                     await CategoryService.GetCountCategories(), CategoryConverter.ConvertToCategoryViewModel(categories));

                return View(categoryIndexModel);
            }
            return NotFound();
        }       


        [HttpGet]
        public async Task<IActionResult> RemoveCategory(int? categoryId)
        {
            Category category = new Category();
            if (categoryId.HasValue && (category = await CategoryService.GetCategoryById(categoryId.Value)) != null)
            {
                await CategoryService.RemoveCategoryAsync(category.Id);
                return RedirectToAction(nameof(IndexСategoriesAdmin));
            }

            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int? categoryId)
        {
            Category category = new Category();
            if (categoryId.HasValue && (category = await CategoryService.GetCategoryById(categoryId.Value)) != null)
            {
                return View(CategoryConverter.ConvertToCategoryViewModel(category));
            }

            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel model)
        {
            if(model != null)
            {
                await CategoryService.UpdateCategoryAsync(new Category() { Id = model.Id, Name = model.Name });
            }
            return View(nameof(IndexСategoriesAdmin));
        }
    }
}