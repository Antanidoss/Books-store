using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Service.Interfaces;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Interfaces.Managers;
using BooksStore.Web.Models.Pagination;
using BooksStore.Web.Models.ViewModel.CreateModel;
using BooksStore.Web.Models.ViewModel.ReadModel;
using BooksStore.Web.Models.ViewModel.UpdateModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.Managers
{
    public class BookManager : IBookManager
    {
        private readonly IBookService _bookService;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ICurrentUser _currentUser;

        public BookManager(IBookService bookService, IMapper mapper, IHttpContextAccessor httpContextAccessor, ICurrentUser currentUser)
        {
            _bookService = bookService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _currentUser = currentUser;
        }

        public async Task AddBookAsync(BookCreateModel bookCreateModel)
        {
            await _bookService.AddBookAsync(_mapper.Map<BookDTO>(bookCreateModel));
        }

        public async Task<BookViewModel> GetBookByIdAsync(int bookId)
        {
            if (bookId <= 0)
            {
                throw new ArgumentException("Id не может быть равен или меньше нуля");
            }

            var book = _mapper.Map<BookViewModel>(await _bookService.GetBookByIdAsync(bookId));
            await BookInBasketAsync(book);
            return book;
        }

        public async Task<IEnumerable<BookViewModel>> GetBooksAsync(int pageNum)
        {
            if(pageNum <= 0)
            {
                throw new ArgumentException("Номер страницы не может быть равен или меньше нуля");
            }

            int pageSize = PageSizes.Books;
            var books = _mapper.Map<IEnumerable<BookViewModel>>(await _bookService.GetBooks((pageNum - 1) * pageSize, pageSize));

            foreach(var book in books)
            {
                await BookInBasketAsync(book);
            }
            
            return books;
        }

        public async Task RemoveBookAsync(int bookId)
        {
            await _bookService.RemoveBookAsync(bookId);
        }

        public async Task UpdateBookAsync(BookUpdateModel bookUpdateModel)
        {
            await _bookService.UpdateBookAsync(_mapper.Map<BookDTO>(bookUpdateModel));
        }

        private async Task BookInBasketAsync(BookViewModel book)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                int basketid = (await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext)).BasketId;
                
                if (await _bookService.IsBookInBasketAsync(basketid, book.Id))
                {
                    book.IsAddToBasket = true;
                }              
            }
        }

        public async Task<IEnumerable<BookViewModel>> GetBooksByNameAsync(int pageNum, string bookName)
        {
            if (pageNum <= 0)
            {
                throw new ArgumentException("Номер страницы не может быть равен или меньше нуля");
            }

            int pageSize = PageSizes.Books;
            return _mapper.Map<IEnumerable<BookViewModel>>
                (await _bookService.GetBooksByNameAsync((pageNum - 1) * pageSize, pageSize, bookName));
        }

        public async Task<IEnumerable<BookViewModel>> GetBooksByCategoryAsync(int pageNum, int categoryId)
        {
            if(pageNum <= 0)
            {
                throw new ArgumentException("Номер страницы не может быть равен или меньше нуля");
            }

            int pageSize = PageSizes.Books;
            return _mapper.Map<IEnumerable<BookViewModel>>(
                await _bookService.GetBooksByCategoryAsync((pageNum - 1) * pageSize, pageSize, categoryId));
        }

        public async Task<int> GetCountAsync()
        {
            return await _bookService.GetCountBooks();
        }
    }
}
