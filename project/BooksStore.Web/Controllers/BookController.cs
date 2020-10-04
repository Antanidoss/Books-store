using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.Index;
using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Web.Controllers
{
    public class BookController : Controller
    {
        IBookService BookService { get; set; }
        IWebHostEnvironment AppEnvironment { get; set; }
        ICurrentUser CurrentUser { get; set; }
        IMapper Mapper { get; set; }

        public BookController(IBookService bookService, IWebHostEnvironment appEnvironment, ICurrentUser currentUser, IMapper mapper)
        {
            BookService = bookService;
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
            if (!bookId.HasValue)
            {
                return NotFound();
            }
            var book = await BookService.GetBookByIdAsync(bookId.Value);
                                
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
                
                var booksCategory = await BookService.GetBookByCategoryAsync(categoryId.Value);
                                                  
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
    }
}