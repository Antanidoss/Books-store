using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Core.BasketModel;
using BooksStore.Core.BookBasketJunctionModel;
using BooksStore.Core.BookModel;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Cache;
using BooksStore.Web.Interface.Converter;
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
        ICurrentUser AppUserService { get; set; }
        IBookConverter BookConverter { get; set; }
        IMemoryCache Cache { get; set; }
        IBookService BookService { get; set; }

        public BasketController(IBasketService basketService, ICurrentUser appUserService , IBookConverter bookConverter , 
            IMemoryCache cache)
        {
            BasketService = basketService;
            AppUserService = appUserService;
            BookConverter = bookConverter;
            Cache = cache;
        }


        [HttpGet]
        public async Task<IActionResult> IndexBasket(int pageNum = 1)
        {
            if (pageNum >= 1)
            {
                int basketId = await GetCurrentBasketId();
                BasketViewModel basketViewModel = new BasketViewModel();
                int pageSize = 6;

                if (!Cache.TryGetValue(CacheKeys.GetBasketKey(basketId), out List<BookBasketJunction> basketBooks))
                {
                    var curBasket = await BasketService.GetBasketByIdAsync(basketId);
                    if (curBasket.BookBaskets.Count() != 0)
                    {
                        Cache.Set(CacheKeys.GetBasketKey(basketId), curBasket.BookBaskets, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheTime.GetBasketCacheTime())
                        });
                        basketBooks = curBasket.BookBaskets.ToList();
                    }                    
                }

                var books = BookConverter.ConvertToBookViewModel(basketBooks
                    ?.Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => p.Book) ?? new List<Book>());

                basketViewModel.BookIndexModel = new IndexViewModel<BookViewModel>(pageNum, pageSize, basketBooks?.Count() ?? 0, books);

                return View(basketViewModel);
            }
            return BadRequest("Некорректные данные в запросе");
        }


        [HttpPost]
        public async Task<IActionResult> AddBasketBook(int? bookId , string returnUrl = "")
        {
            if (bookId.HasValue)
            {
                int basketId = await GetCurrentBasketId();

                await BasketService.AddBasketBookAsync(basketId, bookId.Value);

                RemoveBasketBookCache(basketId);                

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
                int basketId = await GetCurrentBasketId();

                await BasketService.RemoveBasketBookAsync(basketId, bookId.Value);

                RemoveBasketBookCache(basketId);
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
            int basketId = await GetCurrentBasketId();

            await BasketService.RemoveAllBasketBooksAsync(basketId);

            RemoveBasketBookCache(basketId);

            return RedirectToAction(nameof(IndexBasket));
        }


        private void RemoveBasketBookCache(int basketId)
        {
            if(Cache.TryGetValue(CacheKeys.GetBasketKey(basketId), out List<BookBasketJunction> basketbook))
            {
                Cache.Remove(CacheKeys.GetBasketKey(basketId));
            }
        }


        private async Task<int> GetCurrentBasketId()
        {
            return (await AppUserService.GetCurrentAppUser(HttpContext)).BasketId;
        }
    }
}