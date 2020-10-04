using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Interfaces;
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
        IBasketService BasketService { get; set; }
        ICurrentUser CurrentUser { get; set; }
        IBookService BookService { get; set; }
        IMapper Mapper { get; set; }

        public BasketController(IBasketService basketService, ICurrentUser currentUser, IMapper mapper)
        {
            BasketService = basketService;
            CurrentUser = currentUser;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> IndexBasket(int pageNum = 1)
        {
            if (pageNum <= 0)
            {
                return BadRequest("Некорректные данные в запросе");
            }
            AppUserDTO curUser = await CurrentUser.GetCurrentUser(HttpContext);
            
            BasketDTO curBasket = await BasketService.GetBasketByIdAsync(curUser.BasketId);
                                                
            int pageSize = PageSizes.Basket;

            var books = Mapper.Map<IEnumerable<BookViewModel>>(curBasket.BasketBooks
                ?.Skip((pageNum - 1) * pageSize)
                .Take(pageSize));

            var basketViewModel = new BasketViewModel()
            {
                BookIndexModel = new IndexViewModel<BookViewModel>(pageNum, pageSize, curBasket?.BasketBooks.Count() ?? 0, books)
            };

            return View(basketViewModel);                        
        }


        [HttpPost]
        public async Task<IActionResult> AddBasketBook(int? bookId , string returnUrl = "")
        {
            if (!bookId.HasValue)
            {
                return NotFound(); 
            }
            AppUserDTO curUser = await CurrentUser.GetCurrentUser(HttpContext);

            await BasketService.AddBasketBookAsync(curUser.BasketId, bookId.Value);               

            if(string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("IndexBooks", "Book");            
        }


        [HttpGet]
        public async Task<IActionResult> RemoveBasketBook(int? bookId, string returnUrl = "")
        {
            if (bookId.HasValue)
            {
                AppUserDTO curUser = await CurrentUser.GetCurrentUser(HttpContext);

                await BasketService.RemoveBasketBookAsync(curUser.BasketId, bookId.Value);
            }

            if (string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return View(returnUrl);
            }
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

            await BasketService.RemoveAllBasketBooksAsync(curUser.BasketId);

            return RedirectToAction(nameof(IndexBasket));
        }          
    }
}