using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO.Book;
using BooksStore.Services.Interfaces.Services.Base;
using BooksStore.Services.Interfaces.Services.WithCaching;
using BooksStore.Services.Models;
using BooksStore.Web.CacheOptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Services.Implementation.Services.WithCaching
{
    internal sealed class BookCachingService : IBookCachingService
    {
        private readonly IBookService _bookService;

        private readonly ICacheManager _cacheManager;

        private readonly IMapper _mapper;

        public BookCachingService(IBookService bookService, ICacheManager cacheManager, IMapper mapper)
        {
            _bookService = bookService;
            _cacheManager = cacheManager;
            _mapper = mapper;
        }

        public async Task AddBookAsync(BookDTOCreateModel bookCreateModel)
        {
            await _bookService.AddBookAsync(bookCreateModel);
        }

        public async Task<BookDTO> GetBookByIdAsync(int bookId)
        {
            if (_cacheManager.IsSet(CacheKeys.GetBookKey(bookId)))
            {
                var cachingBook = _cacheManager.Get<Book>(CacheKeys.GetBookKey(bookId));
                return _mapper.Map<BookDTO>(cachingBook);
            }

            var book = await _bookService.GetBookByIdAsync(bookId);
            _cacheManager.Set<BookDTO>(CacheKeys.GetBookKey(bookId), book, CacheTimes.BookCacheTime);

            return book;
        }

        public async Task<IEnumerable<BookDTO>> GetBooksAsync(int take, int skip, BookFilterModel filterModel)
        {
            return await _bookService.GetBooksAsync(take, skip, filterModel);
        }

        public async Task<int> GetCountBooksAsync()
        {
            return await _bookService.GetCountBooksAsync();
        }

        public async Task<bool> IsBookInBasketAsync(int basketId, int bookId)
        {
            return await _bookService.IsBookInBasketAsync(basketId, bookId);
        }

        public async Task RemoveBookAsync(int bookId)
        {
            await _bookService.RemoveBookAsync(bookId);

            _cacheManager.Remove(CacheKeys.GetBookKey(bookId));
        }

        public async Task UpdateBookAsync(BookDTO book)
        {
            await _bookService.UpdateBookAsync(book);

            _cacheManager.Remove(CacheKeys.GetBookKey(book.Id));
        }
    }
}
