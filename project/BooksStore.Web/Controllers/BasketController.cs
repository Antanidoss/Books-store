using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.Index;
using BooksStore.Web.Models.ViewModel.ReadModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketManager _basketManager;
        ICurrentUser CurrentUser { get; set; }

        public BasketController(IBasketManager basketManager, ICurrentUser currentUser)
        {
            _basketManager = basketManager;
            CurrentUser = currentUser;
        }

        [HttpGet]
        public async Task<IActionResult> IndexBasket(int pageNum = 1)
        {            
            return View(await _basketManager.GetBasketAsync(pageNum));                        
        }

        [HttpPost]
        public async Task<IActionResult> AddBasketBook(int? bookId)
        {           
            await _basketManager.AddBasketBookAsync(bookId.Value);               
            
            return RedirectToAction("IndexBooks", "Book");            
        }

        [HttpGet]
        public async Task<IActionResult> RemoveBasketBook(int? bookId, string returnUrl = "")
        {                       
            await _basketManager.RemoveBasketBookAsync(bookId.Value);
                       
            return RedirectToAction(nameof(IndexBasket));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveBasketBooks(IEnumerable<int> bookIds)
        {
            foreach(int bookId in bookIds)
            {
                await RemoveBasketBook(bookId);
            }
            return RedirectToAction(nameof(IndexBasket));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveAllBasketBooks()
        {
            AppUserDTO curUser = await CurrentUser.GetCurrentUser(HttpContext);

            await _basketManager.RemoveAllBasketBooksAsync();

            return RedirectToAction(nameof(IndexBasket));
        }          
    }
}