using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.CreateModel;
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
        private readonly IBookViewModelService _bookManager;

        private readonly IWebHostEnvironment _appEnvironment;

        public BookController(IBookViewModelService bookManager, IWebHostEnvironment appEnvironment)
        {
            _bookManager = bookManager;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> IndexBooks(int pageNum = 1, BookListViewModel bookList = null)
        {           
            if (bookList?.BookIndexModel?.Objects == null)
            {
                bookList = new BookListViewModel(pageNum, PageSizes.Books, await _bookManager.GetCountAsync(), 
                    await _bookManager.GetBooksAsync(pageNum));
            }               

            return View(bookList);                       
        }

        [HttpGet]
        public async Task<IActionResult> IndexBook(int? bookId)
        {            
            var book = await _bookManager.GetBookByIdAsync(bookId.Value);                                           

            return View(book);                        
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
        public async Task<IActionResult> AddBook(BookCreateModel model, [Required(ErrorMessage = "Выберите изображения")] IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                string path = "/img/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                model.ImgPath = path;
                await _bookManager.AddBookAsync(model);

                return RedirectToAction(nameof(IndexBooksAdmin), "Book");
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> RemoveBook(int? bookId)
        {
            if (bookId.HasValue)
            {
                await _bookManager.RemoveBookAsync(bookId.Value);          

                return RedirectToAction(nameof(IndexBooksAdmin), "Book");
            }
            return View(StatusCode(404));
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateBook(int? bookId)
        {                       
            return View(await _bookManager.GetBookByIdAsync(bookId.Value));            
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateBook(BookUpdateModel model)
        {
            await _bookManager.UpdateBookAsync(model);
            return RedirectToAction("IndexBooksAdmin", "Book");           
        }

        [HttpGet]
        public async Task<IActionResult> IndexByCategory(int? categoryId, int pageNum = 1)
        {                     
            var booksCategory = await _bookManager.GetBooksByCategoryAsync(pageNum, categoryId.Value);
                                              
            BookListViewModel indexBookModel = new BookListViewModel(pageNum, PageSizes.Books, booksCategory.Count(), booksCategory);

            return View(indexBookModel);            
        }

        public async Task<IActionResult> IndexBooksByName(string bookName, int pageNum = 1)
        {           
            var books = await _bookManager.GetBooksByNameAsync(pageNum, bookName);

            BookListViewModel indexBookModel = new BookListViewModel(pageNum, PageSizes.Books, books.Count(), books);

            return View(indexBookModel);           
        }        
    }
}