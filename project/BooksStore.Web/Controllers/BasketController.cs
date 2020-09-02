using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using BooksStore.Service.Interfaces.Identity;
using BooksStore.Web.Cache;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Models.ViewModels.Basket;
using BooksStore.Web.Models.ViewModels.Book;
using BooksStore.Web.Models.ViewModels.Index;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BooksStore.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        IBasketService BasketService { get; set; }
        ICurrentUser CurrentUser { get; set; }
        IMemoryCache Cache { get; set; }
        IBookService BookService { get; set; }
        IMapper Mapper { get; set; }

        public BasketController(IBasketService basketService, ICurrentUser currentUser, IMemoryCache cache, IMapper mapper)
        {
            BasketService = basketService;
            CurrentUser = currentUser;
            Cache = cache;
            Mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> IndexBasket(int pageNum = 1)
        {
            if (pageNum >= 1)
            {
                AppUserDTO curUser = await CurrentUser.GetCurrentUser(HttpContext);
                BasketDTO curBasket = new BasketDTO();
                BasketViewModel basketViewModel = new BasketViewModel();
                int pageSize = 6;

                if (!Cache.TryGetValue(CacheKeys.GetBasketKey(curUser.BasketId), out BasketDTO basket))
                {
                    curBasket = await BasketService.GetBasketByIdAsync(curUser.BasketId);

                    if (curBasket.BasketBooks.Count() != 0)
                    {
                        Cache.Set(CacheKeys.GetBasketKey(curBasket.Id), curBasket.BasketBooks, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheTime.GetBasketCacheTime())
                        });
                    }                    
                }

                var books = Mapper.Map<IEnumerable<BookViewModel>>(curBasket.BasketBooks
                    ?.Skip((pageNum - 1) * pageSize)
                    .Take(pageSize));

                basketViewModel.BookIndexModel = new IndexViewModel<BookViewModel>(pageNum, pageSize, curBasket?.BasketBooks.Count() ?? 0, books);

                return View(basketViewModel);
            }
            return BadRequest("Некорректные данные в запросе");
        }


        [HttpPost]
        public async Task<IActionResult> AddBasketBook(int? bookId , string returnUrl = "")
        {
            if (bookId.HasValue)
            {
                AppUserDTO curUser = await CurrentUser.GetCurrentUser(HttpContext);

                await BasketService.AddBasketBookAsync(curUser.BasketId, bookId.Value);

                RemoveBasketBookCache(curUser.BasketId);                

                if(string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("IndexBooks", "Book");
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> RemoveBasketBook(int? bookId, string returnUrl = "")
        {
            if (bookId.HasValue)
            {
                AppUserDTO curUser = await CurrentUser.GetCurrentUser(HttpContext);

                await BasketService.RemoveBasketBookAsync(curUser.BasketId, bookId.Value);

                RemoveBasketBookCache(curUser.BasketId);
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

            RemoveBasketBookCache(curUser.BasketId);

            return RedirectToAction(nameof(IndexBasket));
        }


        private void RemoveBasketBookCache(int basketId)
        {
            if(Cache.TryGetValue(CacheKeys.GetBasketKey(basketId), out List<BookDTO> basketbook))
            {
                Cache.Remove(CacheKeys.GetBasketKey(basketId));
            }
        }       
    }
}