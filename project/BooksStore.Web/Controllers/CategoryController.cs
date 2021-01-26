using System.Threading.Tasks;
using BooksStore.Web.Filters;
using BooksStore.Web.Interfaces.Managers;
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
        private readonly ICategoryViewModelService _categoryService;

        public CategoryController(ICategoryViewModelService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult AddCategory() => View();
        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<IActionResult> AddCategory(CategoryCreateModel model)
        {            
            await _categoryService.AddCategoryAsync(model);

            return RedirectToAction("IndexСategoriesAdmin", "Category");
        }

        [HttpGet]
        public async Task<IActionResult> IndexСategoriesAdmin(int pageNum = 1)
        {
            return await IndexCategories(pageNum);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IndexCategories(int pageNum = 1)
        {
            var categories = await _categoryService.GetCategories(pageNum);
            
            IndexViewModel<CategoryViewModel> categoryIndexModel = new IndexViewModel<CategoryViewModel>(pageNum, PageSizes.Categories,
                 await _categoryService.GetCountCategories(), categories);

            return View(categoryIndexModel);
            
        }       

        [HttpPost]
        public async Task<IActionResult> RemoveCategory(int? categoryId)
        {
            if (!categoryId.HasValue)
            {
                return StatusCode(404);
            }

            await _categoryService.RemoveCategoryAsync(categoryId.Value);

            return RedirectToAction(nameof(IndexСategoriesAdmin));         
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int? categoryId)
        {
            if (!categoryId.HasValue)
            {
                return StatusCode(404);
            }

            return View(await _categoryService.GetCategoryById(categoryId.Value));
        }
        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateModel model)
        {
            await _categoryService.UpdateCategoryAsync(model);

            return RedirectToAction(nameof(IndexCategories));
        }
    }
}