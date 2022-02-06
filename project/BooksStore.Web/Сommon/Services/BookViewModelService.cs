using AutoMapper;
using BooksStore.Service.Models;
using BooksStore.Services.DTO.Book;
using BooksStore.Services.Interfaces;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Interfaces.Services;
using BooksStore.Web.Сommon.Pagination;
using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using BooksStore.Web.Сommon.ViewModel.UpdateModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BooksStore.Web.Сommon.Services
{
    public class BookViewModelService : IBookViewModelService
    {
        private readonly IBookService _bookService;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ICurrentUser _currentUser;

        private readonly IWebHostEnvironment _appEnvironment;

        public BookViewModelService(IBookService bookService, IMapper mapper, IHttpContextAccessor httpContextAccessor, ICurrentUser currentUser, IWebHostEnvironment appEnvironment)
        {
            _bookService = bookService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _currentUser = currentUser;
            _appEnvironment = appEnvironment;
        }

        public async Task AddBookAsync(BookCreateModel bookCreateModel)
        {
            string path = "/img/" + bookCreateModel.ImgFile.FileName;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await bookCreateModel.ImgFile.CopyToAsync(fileStream);
            }

            bookCreateModel.ImgPath = path;

            await _bookService.AddBookAsync(_mapper.Map<BookDTOCreateModel>(bookCreateModel));
        }

        public async Task<BookViewModel> GetBookByIdAsync(int bookId)
        {
            var book = _mapper.Map<BookViewModel>(await _bookService.GetBookByIdAsync(bookId));
            await BookInBasketAsync(book);

            return book;
        }

        public async Task<IEnumerable<BookViewModel>> GetBooksAsync(int pageNum)
        {
            if (!PaginationInfo.PageNumberIsValid(pageNum))
            {
                throw new ArgumentException("Номер страницы не может быть равен или меньше нуля");
            }

            int take = PaginationInfo.GetCountTakeItems(pageNum, PageSizes.Books);
            var books = _mapper.Map<IEnumerable<BookViewModel>>(await _bookService.GetBooksAsync(PageSizes.Books, take));

            foreach (var book in books)
            {
                await BookInBasketAsync(book);
            }

            return books;
        }

        public async Task<IEnumerable<BookViewModel>> GetBooksWithFilter(int pageNum, BookFilterModel filterModel)
        {
            if (!PaginationInfo.PageNumberIsValid(pageNum))
            {
                throw new ArgumentException("Номер страницы не может быть равен или меньше нуля");
            }

            if (filterModel.CategoryIds != null)
                filterModel.CategoryIds.RemoveAll(i => i == default);

            int take = PaginationInfo.GetCountTakeItems(pageNum, PageSizes.Books);
            var books = _mapper.Map<IEnumerable<BookViewModel>>(await _bookService.GetBooksWithFilterAsync(PageSizes.Books, take, filterModel));

            foreach (var book in books)
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

        public async Task<int> GetCountAsync()
        {
            return await _bookService.GetCountBooksAsync();
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
    }
}