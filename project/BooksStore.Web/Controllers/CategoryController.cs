using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Converter._Category;
using BooksStore.Web.Models.ViewModels.Category;
using BooksStore.Web.Models.ViewModels.Index;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        ICategoryService CategoryService { get; set; }
        public CategoryController(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }


        [HttpGet]
        public IActionResult AddCategory() => View();
        [HttpPost]
        public async Task<IActionResult> AddCategory([Required(ErrorMessage ="Введите название категории")]string categoryName)
        {
            if (ModelState.IsValid)
            {
                await CategoryService.AddCategoryAsync(new CategoryDTO() { Name = categoryName });

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
                     await CategoryService.GetCountCategories(), CategoryVMConverter.ConvertToCategoryViewModel(categories));

                return View(categoryIndexModel);
            }
            return NotFound();
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
                return View(CategoryVMConverter.ConvertToCategoryViewModel(category));
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel model)
        {
            if(model != null)
            {
                await CategoryService.UpdateCategoryAsync(new CategoryDTO() { Id = model.Id, Name = model.Name });
            }
            return View(nameof(IndexСategoriesAdmin));
        }
    }
}