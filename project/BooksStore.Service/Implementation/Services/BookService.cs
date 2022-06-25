using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.Models;
using BooksStore.Services.DTO.Book;
using BooksStore.Services.Interfaces;
using BooksStore.Web.CacheOptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using BooksStore.Common.Exceptions;
using QueryableFilterSpecification;
using QueryableFilterSpecification.Implementation;
using BooksStore.Services.Implementation.Filters.BookFilters;
using QueryableFilterSpecification.Interfaces;

namespace BooksStore.Services.Implementation.Services
{
    internal sealed class BookService : IBookService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IMapper _mapper;

        private readonly ICacheManager _cacheManager;

        public BookService(IMapper mapper, ICacheManager cacheManager, IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }

        public async Task AddBookAsync(BookDTOCreateModel bookCreateModel)
        {
            await _repositoryFactory.CreateBookRepository().AddAsync(_mapper.Map<BookDTOCreateModel, Book>(bookCreateModel));
        }

        public async Task<BookDTO> GetBookByIdAsync(int bookId)
        {
            if (_cacheManager.IsSet(CacheKeys.GetBookKey(bookId)))
                return _mapper.Map<BookDTO>(_cacheManager.Get<Book>(CacheKeys.GetBookKey(bookId)));

            var filter = new BookByIdFilterSpec(bookId);
            var book = await _repositoryFactory.CreateBookRepository().GetAsync(filter);

            if (book == null)
                throw new NotFoundException(nameof(Book), book);

            return _mapper.Map<BookDTO>(book);
        }

        public async Task<IEnumerable<BookDTO>> GetBooksAsync(int skip, int take)
        {
            var books = await _repositoryFactory.CreateBookRepository().GetAsync(skip, take);

            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<IEnumerable<BookDTO>> GetBooksWithFilterAsync(int take, int skip, BookFilterModel filterModel)
        {
            if (filterModel.FilterIsNull())
                return await GetBooksAsync(skip, take);

            IQueryableFilterSpec<Book> filter = new BookPriceFilterSpecification(filterModel.BookPriceFrom, filterModel.BookPriceTo);

            if (!string.IsNullOrEmpty(filterModel.BookName))
                filter = filter.And(new BookNameFilterSpecification(filterModel.BookName));

            if (filterModel.CategoryIds != null && filterModel.CategoryIds.Any())
                filter = filter.And(new BookCategoryFilterSpecification(filterModel.CategoryIds));

            var result = await _repositoryFactory.CreateBookRepository().GetAsync(skip, take, filter);

            return _mapper.Map<IEnumerable<BookDTO>>(result);
        }

        public async Task RemoveBookAsync(int bookId)
        {
            var filter = new BookByIdFilterSpec(bookId);
            var book = await _repositoryFactory.CreateBookRepository().GetAsync(filter);

            if (book != default)
            {
                await _repositoryFactory.CreateBookRepository().RemoveAsync(book);
                _cacheManager.Remove(CacheKeys.GetBookKey(bookId));
            }
        }

        public async Task UpdateBookAsync(BookDTO bookDTO)
        {
            var bookRepository = _repositoryFactory.CreateBookRepository();
            var filter = new BookByIdFilterSpec(bookDTO.Id);
            var book = await bookRepository.GetAsync(filter);

            book.Descriptions = bookDTO.Descriptions;
            book.NumberOfPages = bookDTO.NumberOfPages;
            book.Title = bookDTO.Title;
            book.InStock = bookDTO.InStock;
            book.Price = bookDTO.Price;

            await bookRepository.UpdateAsync(book);

            _cacheManager.Remove(CacheKeys.GetBookKey(bookDTO.Id));
        }

        public async Task<bool> IsBookInBasketAsync(int basketId, int bookId)
        {
            var filter = new BookByIdFilterSpec(bookId);
            var book = await _repositoryFactory.CreateBookRepository().GetAsync(filter);

            return book != null && book.BookBaskets.FirstOrDefault(p => p.BookId == bookId && basketId == p.BasketId) != null;
        }

        public async Task<int> GetCountBooksAsync()
        {
            return await _repositoryFactory.CreateBookRepository().GetCountAsync();
        }
    }
}