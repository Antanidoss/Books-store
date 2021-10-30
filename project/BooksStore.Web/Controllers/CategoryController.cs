using System.Threading.Tasks;
using BooksStore.Web.Filters;
using BooksStore.Web.Interfaces.Services;
using BooksStore.Web.Сommon.Pagination;
using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.Index;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using BooksStore.Web.Сommon.ViewModel.UpdateModel;
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

            return View(new IndexViewModel<CategoryViewModel>(pageNum, PageSizes.Categories, await _categoryService.GetCountCategories(), categories));
        }

        [HttpPost]
        [IdValidationFilter("categoryId")]
        public async Task<IActionResult> RemoveCategory(int? categoryId)
        {
            await _categoryService.RemoveCategoryAsync(categoryId.Value);

            return RedirectToAction(nameof(IndexСategoriesAdmin));
        }

        [HttpGet]
        [IdValidationFilter("categoryId")]
        public async Task<IActionResult> UpdateCategory(int? categoryId)
        {
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