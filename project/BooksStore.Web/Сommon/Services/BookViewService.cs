using AutoMapper;
using BooksStore.Services.Models;
using BooksStore.Services.DTO.Book;
using BooksStore.Web.Interfaces;
using BooksStore.Web.Interfaces.Services;
using BooksStore.Web.Сommon.Pagination;
using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using BooksStore.Web.Сommon.ViewModel.UpdateModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using BooksStore.Services.Interfaces.Services.WithCaching;

namespace BooksStore.Web.Сommon.Services
{
    public class BookViewService : IBookViewModelService
    {
        private readonly IBookCachingService _bookService;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ICurrentUser _currentUser;

        public BookViewService(IBookCachingService bookService, IMapper mapper, IHttpContextAccessor httpContextAccessor, ICurrentUser currentUser)
        {
            _bookService = bookService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _currentUser = currentUser;
        }

        public async Task AddBookAsync(BookCreateModel bookCreateModel)
        {
            await _bookService.AddBookAsync(_mapper.Map<BookDTOCreateModel>(bookCreateModel));
        }

        public async Task<BookViewModel> GetBookByIdAsync(int bookId)
        {
            var book = _mapper.Map<BookViewModel>(await _bookService.GetBookByIdAsync(bookId));
            await SetIsAddToBasketStatus(book);

            return book;
        }

        public async Task<IEnumerable<BookViewModel>> GetBooks(int pageNum, BookFilterModel filterModel)
        {
            if (filterModel.CategoryIds != null)
                filterModel.CategoryIds.RemoveAll(i => i == default);

            var take = PageSizes.Books;
            var skip = PaginationInfo.GetCountSkipItems(pageNum, take);
            var books = _mapper.Map<IEnumerable<BookViewModel>>(await _bookService.GetBooksAsync(skip, take, filterModel));

            foreach (var book in books)
                await SetIsAddToBasketStatus(book);

            return books;
        }

        public async Task RemoveBookAsync(int bookId)
        {
            await _bookService.RemoveBookAsync(bookId);
        }

        public async Task UpdateBookAsync(BookUpdateModel bookUpdateModel)
        {
            var bookDTO = _mapper.Map<BookDTO>(bookUpdateModel);

            await _bookService.UpdateBookAsync(bookDTO);
        }

        public async Task<int> GetCountAsync()
        {
            return await _bookService.GetCountBooksAsync();
        }

        private async Task SetIsAddToBasketStatus(BookViewModel book)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return;

            int basketid = (await _currentUser.GetCurrentUser(_httpContextAccessor.HttpContext)).BasketId;
            book.IsAddToBasket = await _bookService.IsBookInBasketAsync(basketid, book.Id);
        }
    }
}