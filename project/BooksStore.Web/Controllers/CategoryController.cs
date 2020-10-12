using System.Threading.Tasks;
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
        private readonly ICategoryManager _categoryManager;
        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet]
        public IActionResult AddCategory() => View();
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryCreateModel categoryCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryCreateModel);
            }

            await _categoryManager.AddCategoryAsync(categoryCreateModel);

            return RedirectToAction("IndexСategoriesAdmin", "Category");
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
            int pageSize = PageSizes.Categories;

            var categories = await _categoryManager.GetCategories(pageNum);
            
            IndexViewModel<CategoryViewModel> categoryIndexModel = new IndexViewModel<CategoryViewModel>(pageNum, pageSize,
                 await _categoryManager.GetCountCategories(), categories);

            return View(categoryIndexModel);
            
        }       

        [HttpGet]
        public async Task<IActionResult> RemoveCategory(int? categoryId)
        {        
            await _categoryManager.RemoveCategoryAsync(categoryId.Value);

            return RedirectToAction(nameof(IndexСategoriesAdmin));         
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int? categoryId)
        {
            return View(await _categoryManager.GetCategoryById(categoryId.Value));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateModel model)
        {
            await _categoryManager.UpdateCategoryAsync(model);

            return RedirectToAction(nameof(IndexCategories));
        }
    }
}