using System.Collections.Generic;
using System.Threading.Tasks;
using BooksStore.Web.Filters;
using BooksStore.Web.Interfaces.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketViewModelService _basketService;

        public BasketController(IBasketViewModelService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexBasket(int pageNum = 1)
        {            
            return View(await _basketService.GetBasketAsync(pageNum));                        
        }

        [HttpPost]
        [IdValidationFilter("bookId")]
        public async Task<IActionResult> AddBasketBook(int? bookId)
        {            
            await _basketService.AddBasketBookAsync(bookId.Value);               
            
            return RedirectToAction("IndexBooks", "Book");            
        }

        [HttpPost]
        [IdValidationFilter("bookId")]
        public async Task<IActionResult> RemoveBasketBook(int? bookId)
        {           
            await _basketService.RemoveBasketBookAsync(bookId.Value);
                       
            return RedirectToAction(nameof(IndexBasket));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBasketBooks(IEnumerable<int> bookIds)
        {
            foreach(int bookId in bookIds)
            {
                await _basketService.RemoveBasketBookAsync(bookId);
            }

            return RedirectToAction(nameof(IndexBasket));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAllBasketBooks()
        {
            await _basketService.RemoveAllBasketBooksAsync();

            return RedirectToAction(nameof(IndexBasket));
        }          
    }
}