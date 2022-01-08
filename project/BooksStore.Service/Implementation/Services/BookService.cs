using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Exceptions;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Service.Interfaces.FilterBuilders;
using BooksStore.Service.Models;
using BooksStore.Services.DTO.Book;
using BooksStore.Services.Interfaces;
using BooksStore.Web.CacheOptions;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BooksStore.Services
{
    public class BookService : IBookService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IMapper _mapper;

        private readonly ICacheManager _cacheManager;

        private readonly IBookFilterBuilder _bookFilterBuilder;

        public BookService(IMapper mapper, ICacheManager cacheManager, IRepositoryFactory repositoryFactory, IBookFilterBuilder bookFilterBuilder)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _cacheManager = cacheManager;
            _bookFilterBuilder = bookFilterBuilder;
        }

        public async Task AddBookAsync(BookDTOCreateModel bookCreateModel)
        {
            await _repositoryFactory.CreateBookRepository().AddAsync(_mapper.Map<BookDTOCreateModel, Book>(bookCreateModel));
        }

        public async Task<BookDTO> GetBookByIdAsync(int bookId)
        {
            if (_cacheManager.IsSet(CacheKeys.GetBookKey(bookId)))
            {
                return _mapper.Map<BookDTO>(_cacheManager.Get<Book>(CacheKeys.GetBookKey(bookId)));
            }

            var book = await _repositoryFactory.CreateBookRepository().GetByIdAsync(bookId);

            if (book == null)
            {
                throw new NotFoundException(nameof(Book), book);
            }

            return _mapper.Map<BookDTO>(book);
        }

        public async Task<IEnumerable<BookDTO>> GetBooksAsync(int skip, int take)
        {
            var books = await _repositoryFactory.CreateBookRepository().GetAsync(skip, take);

            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<IEnumerable<BookDTO>> GetBooksWithFilterAsync(int take, int skip, FilterModel filterModel)
        {
            if (filterModel.FilterIsNull())
            {
                return await GetBooksAsync(skip, take);
            }

            filterModel.BookName = string.IsNullOrEmpty(filterModel.BookName) ? string.Empty : filterModel.BookName.ToLower().Replace(" ", "");

            var result = await _repositoryFactory.CreateBookRepository().GetAsync(skip, take, GetFilterCondition(filterModel));

            return _mapper.Map<IEnumerable<BookDTO>>(result);
        }

        public async Task RemoveBookAsync(int bookId)
        {
            var book = await _repositoryFactory.CreateBookRepository().GetByIdAsync(bookId);

            if (book != default)
            {
                await _repositoryFactory.CreateBookRepository().RemoveAsync(book);
                _cacheManager.Remove(CacheKeys.GetBookKey(bookId));
            }
        }

        public async Task UpdateBookAsync(BookDTO bookDTO)
        {
            await _repositoryFactory.CreateBookRepository().UpdateAsync(_mapper.Map<Book>(bookDTO));
            _cacheManager.Remove(CacheKeys.GetBookKey(bookDTO.Id));
        }

        public async Task<bool> IsBookInBasketAsync(int basketId, int bookId)
        {
            var book = await _repositoryFactory.CreateBookRepository().GetByIdAsync(bookId);

            return book != null && (book.BookBaskets.FirstOrDefault(p => p.BookId == bookId && basketId == p.BasketId)) != default;
        }

        public async Task<int> GetCountBooksAsync()
        {
            return await _repositoryFactory.CreateBookRepository().GetCountAsync();
        }

        private Expression<Func<Book, bool>> GetFilterCondition(FilterModel filterModel)
        {
            if (!string.IsNullOrEmpty(filterModel.BookName))
            {
                _bookFilterBuilder.BuildWithFilterName(filterModel.BookName);
            }

            filterModel.CategoryIds.RemoveAll(i => i == default);
            if (filterModel.CategoryIds.Any())
            {
                _bookFilterBuilder.BuildWithFilterCategories(filterModel.CategoryIds);
            }

            _bookFilterBuilder.BuildWithFilterPrice(filterModel.BookPriceFrom, filterModel.BookPriceTo);

            return _bookFilterBuilder.GetResult();
        }
    }
}