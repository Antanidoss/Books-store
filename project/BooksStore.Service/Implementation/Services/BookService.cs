using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Exceptions;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO.Book;
using BooksStore.Services.Interfaces;
using BooksStore.Web.CacheOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Services
{
    public class BookService : IBookService
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

        public async Task<IEnumerable<BookDTO>> GetBooks(int skip, int take)
        {
            var books = await _repositoryFactory.CreateBookRepository().GetAsync(skip, take);

            return _mapper.Map<IEnumerable<BookDTO>>(books);
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

            return book != null && (book.BookBaskets.FirstOrDefault(p => p.BookId == bookId && basketId == p.BasketId)) != default
                ? true
                : false;
        }

        public async Task<IEnumerable<BookDTO>> GetBooksByCategoryAsync(int skip, int take, int categoryId)
        {
            Category category = await _repositoryFactory.CreateCategoryRepository().GetByIdAsync(categoryId);
            if (category == null)
            {
                throw new ArgumentNullException(nameof(Category));
            }

            return _mapper.Map<IEnumerable<BookDTO>>(await _repositoryFactory.CreateBookRepository().GetAsync(skip, take, (b) => b.CategoryId == categoryId));
        }

        public async Task<int> GetCountBooks()
        {
            return await _repositoryFactory.CreateBookRepository().GetCountAsync();
        }

        public async Task<IEnumerable<BookDTO>> GetBooksByNameAsync(int skip, int take, string bookName)
        {
            bookName = bookName.ToLower().Replace(" ", "");
            var books = await _repositoryFactory.CreateBookRepository().GetAsync(skip, take, (b) => b.Title.ToLower().Replace(" ", "") == bookName);

            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }
    }
}