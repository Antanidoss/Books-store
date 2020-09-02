using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Cache;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Models.CreateModels;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModels;
using BooksStore.Web.Models.ViewModels.Index;
using BooksStore.Web.Models.ViewModels.UpdateModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BooksStore.Web.Controllers
{
    public class BookController : Controller
    {
        IBookService BookService { get; set; }
        IMemoryCache Cache { get; set; }
        IWebHostEnvironment AppEnvironment { get; set; }
        ICurrentUser CurrentUser { get; set; }
        IMapper Mapper { get; set; }

        public BookController(IBookService bookService, IMemoryCache cache, IWebHostEnvironment appEnvironment, ICurrentUser currentUser,
            IMapper mapper)
        {
            BookService = bookService;
            Cache = cache;
            AppEnvironment = appEnvironment;
            CurrentUser = currentUser;
            Mapper = mapper;
        }

        public async Task<IActionResult> IndexBooks(int pageNum = 1, IndexViewModel<BookViewModel> indexBookModel = null)
        {
            if (pageNum >= 1)
            {
                if (indexBookModel?.Objects == null)
                {
                    int pageSize = PageSizes.Books;                                        
                                            
                    indexBookModel = new IndexViewModel<BookViewModel>(pageNum, pageSize, await BookService.GetCountBooks(),
                        Mapper.Map<IEnumerable<BookViewModel>>(await BookService.GetBooks((pageNum - 1) * pageSize , pageSize)));
                }

                if (HttpContext.User.Identity.IsAuthenticated && indexBookModel.Objects != null && indexBookModel.Objects.Count() != 0)
                {
                    int basketId = (await CurrentUser.GetCurrentUser(HttpContext)).BasketId;

                    foreach (var book in indexBookModel.Objects)
                    {
                        if (await BookService.IsBookInBasketAsync(basketId, book.Id))
                        {
                            book.IsAddToBasket = true;
                        }
                    }
                }

                return View(indexBookModel);
            }

            return BadRequest("Некорректные данные в запросе");
        }


        [HttpGet]
        public async Task<IActionResult> IndexBook(int? bookId)
        {
            if (bookId.HasValue)
            {
                if (!Cache.TryGetValue(CacheKeys.GetBookKey(bookId.Value), out BookDTO book))
                {
                    book = await BookService.GetBookByIdAsync(bookId.Value);
                    if (book != null)
                    {
                        Cache.Set(CacheKeys.GetBookKey(bookId.Value), book, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheTimes.BookCacheTime)
                        });
                    }
                    else
                    {
                        return NotFound();
                    }
                }

                var bookViewModel = Mapper.Map<BookViewModel>(book);

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    int basketId = (await CurrentUser.GetCurrentUser(HttpContext)).BasketId;
                    if (await BookService.IsBookInBasketAsync(basketId, bookId.Value))
                    {
                        bookViewModel.IsAddToBasket = true;
                    }
                }

                return View(bookViewModel);
            }
            return NotFound();
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> IndexBooksAdmin(int pageNum = 1)
        {
            return await IndexBooks(pageNum);
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddBook() => View();

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddBook(BookCreateModel createModel, [Required(ErrorMessage = "Выберите изображения")] IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                string path = "/img/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(AppEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                var book = Mapper.Map<BookDTO>(createModel);
                book.ImgPath = path;
                await BookService.AddBookAsync(book);

                return RedirectToAction(nameof(IndexBooksAdmin), "Book");
            }
            return View(createModel);
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> RemoveBook(int? bookId)
        {
            if (bookId.HasValue)
            {
                await BookService.RemoveBookAsync(bookId.Value);          
                RemoveBookInCahe(bookId.Value);

                return RedirectToAction(nameof(IndexBooksAdmin), "Book");
            }
            return View(StatusCode(404));
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateBook(int? bookId)
        {
            BookDTO updateBook = new BookDTO();
            if (bookId.HasValue && (updateBook = await BookService.GetBookByIdAsync(bookId.Value)) != null)
            {
                View(Mapper.Map<BookViewModel>(updateBook));
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateBook(BookUpdateModel model)
        {
            if (model != null)
            {
                BookDTO updateBook = await BookService.GetBookByIdAsync(model.Id);
                if (updateBook != null)
                {                   
                    await BookService.UpdateBookAsync(Mapper.Map<BookDTO>(model));

                    RemoveBookInCahe(updateBook.Id);
                }
                return RedirectToAction("IndexBooksAdmin", "Book");
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> IndexByCategory(int? categoryId, int pageNum = 1)
        {
            if (categoryId.HasValue && pageNum >= 1)
            {
                int pageSize = 6;

                if (!Cache.TryGetValue(CacheKeys.GetBooksByCategoryKey(categoryId.Value), out IEnumerable<BookDTO> booksCategory))
                {
                    booksCategory = await BookService.GetBookByCategoryAsync(categoryId.Value);
                    if(booksCategory.Count() != 0)
                    {
                        Cache.Set(CacheKeys.GetBooksByCategoryKey(categoryId.Value), booksCategory, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheTimes.BooksByCategoryCacheTime)
                        });
                    }                   
                }

                IndexViewModel<BookViewModel> indexBookModel = new IndexViewModel<BookViewModel>(pageNum, pageSize, booksCategory.Count(),
                    Mapper.Map<IEnumerable<BookViewModel>>(booksCategory));

                return View(indexBookModel);
            }

            return NotFound();
        }


        public async Task<IActionResult> IndexBooksByName(string bookName, int pageNum = 1)
        {
            if(!string.IsNullOrEmpty(bookName) && pageNum >= 1)
            {
                int pageSize = 6;
                var books = (await BookService.GetBooks((pageNum - 1) * pageSize, pageSize)).Where(p => p.Title == bookName);

                IndexViewModel<BookViewModel> indexBookModel = new IndexViewModel<BookViewModel>(pageNum, pageSize, books.Count(),
                    Mapper.Map<IEnumerable<BookViewModel>>(books));

                return View(indexBookModel);
            }

            return BadRequest("Некорректные данные в запросе");
        }


        private void RemoveBookInCahe(int bookId)
        {
            if (Cache.TryGetValue(CacheKeys.GetBookKey(bookId), out BookDTO book))
            {
                Cache.Remove(CacheKeys.GetBookKey(bookId));
            }
        }
    }
}