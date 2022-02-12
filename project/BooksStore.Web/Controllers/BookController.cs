using System.Threading.Tasks;
using AutoMapper;
using BooksStore.Service.Models;
using BooksStore.Web.Filters;
using BooksStore.Web.Interfaces.Services;
using BooksStore.Web.Сommon.Pagination;
using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using BooksStore.Web.Сommon.ViewModel.UpdateModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class BookController : Controller
    {
        private readonly IBookViewModelService _bookService;

        private readonly IMapper _mapper;

        public BookController(IBookViewModelService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [PageNumValidationFilter]
        public async Task<IActionResult> IndexBooks(BookFilterModel filterModel, int pageNum = 1)
        {
            int booksCount = await _bookService.GetCountAsync();
            var books = await _bookService.GetBooksWithFilter(pageNum, filterModel);

            ViewBag.FilterModel = filterModel;

            return View(new BookListViewModel(pageNum, PageSizes.Books, booksCount, books));
        }

        [HttpGet]
        [AllowAnonymous]
        [IdValidationFilter("bookId")]
        public async Task<IActionResult> IndexBook(int? bookId)
        {
            var book = await _bookService.GetBookByIdAsync(bookId.Value);

            return View(book);
        }

        [HttpGet]
        [PageNumValidationFilter]
        public async Task<IActionResult> IndexBooksAdmin(BookFilterModel filterModel = null, int pageNum = 1)
        {
            return await IndexBooks(filterModel, pageNum);
        }

        [HttpGet]
        public IActionResult AddBook() => View();
        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<IActionResult> AddBook(BookCreateModel model)
        {
            await _bookService.AddBookAsync(model);

            return RedirectToAction(nameof(IndexBooksAdmin));
        }

        [HttpPost]
        [IdValidationFilter("bookId")]
        public async Task<IActionResult> RemoveBook(int? bookId)
        {
            await _bookService.RemoveBookAsync(bookId.Value);

            return RedirectToAction(nameof(IndexBooksAdmin));
        }

        [HttpGet]
        [IdValidationFilter("bookId")]
        public async Task<IActionResult> UpdateBook(int? bookId)
        {
            var book = await _bookService.GetBookByIdAsync(bookId.Value);

            return View(_mapper.Map<BookUpdateModel>(book));
        }
        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<IActionResult> UpdateBook(BookUpdateModel model)
        {
            await _bookService.UpdateBookAsync(model);

            return RedirectToAction(nameof(IndexBooksAdmin));
        }
    }
}