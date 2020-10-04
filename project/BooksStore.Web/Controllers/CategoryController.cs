using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.Index;
using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        ICategoryService CategoryService { get; set; }
        IMapper Mapper { get; set; }
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            CategoryService = categoryService;
            Mapper = mapper;
        }

        [HttpGet]
        public IActionResult AddCategory() => View();
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryCreateModel categoryCreateModel)
        {
            if (ModelState.IsValid)
            {
                await CategoryService.AddCategoryAsync(Mapper.Map<CategoryDTO>(categoryCreateModel));

                return RedirectToAction("IndexСategoriesAdmin", "Category");
            }
            return View(categoryCreateModel);
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
                int pageSize = PageSizes.Categories;

                var categories = (await CategoryService.GetCategories((pageNum - 1) * pageSize, pageSize)).ToList();
                
                IndexViewModel<CategoryViewModel> categoryIndexModel = new IndexViewModel<CategoryViewModel>(pageNum, pageSize,
                     await CategoryService.GetCountCategories(), Mapper.Map<IEnumerable<CategoryViewModel>>(categories));

                return View(categoryIndexModel);
            }
            throw new ArgumentException();
        }       

        [HttpGet]
        public async Task<IActionResult> RemoveCategory(int? categoryId)
        {
            CategoryDTO category = new CategoryDTO();
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
            CategoryDTO category = new CategoryDTO();
            if (categoryId.HasValue && (category = await CategoryService.GetCategoryById(categoryId.Value)) != null)
            {
                return View(Mapper.Map<CategoryViewModel>(category));
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateModel model)
        {
            CategoryDTO categoryDTO = new CategoryDTO();
            if(model != null && (categoryDTO = await CategoryService.GetCategoryById(model.Id)) != null)
            {
                categoryDTO = Mapper.Map<CategoryDTO>(model);
                await CategoryService.UpdateCategoryAsync(categoryDTO);
            }
            return NotFound();
        }
    }
}