using AutoMapper;
using BooksStore.Core.Entities;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Infrastructure.Exceptions;
using BooksStore.Infrastructure.Interfaces;
using BooksStore.Services.DTO;
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
        private readonly IBookRepository _bookRepository;

        private readonly ICategoryRepository _categoryRepository;

        private readonly IAuthorRepository _authorRepository;

        private readonly IMapper _mapper;

        private readonly ICacheManager _cacheManager;

        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository, IAuthorRepository authorRepository,
             IMapper mapper, ICacheManager cacheManager)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }

        public async Task AddBookAsync(BookDTO bookDTO)
        {
            if (bookDTO == null)
            {
                throw new ArgumentNullException(nameof(BookDTO));
            }

            Book book = _mapper.Map<Book>(bookDTO);

            Category category = await _categoryRepository.GetCategoryByName(bookDTO.CategoryName);
            if (category == null)
            {
                category = new Category() { Name = bookDTO.CategoryName };                
            }           

            Author author = await _authorRepository.GetAuthorByName(book.Author.Firstname, book.Author.Surname);
            if (author == null)
            {
                author = new Author() { Firstname = bookDTO.AuthorFirstname, Surname = bookDTO.AuthorSurname };              
            }
          
            book.Category = category;
            book.Author = author;
            await _bookRepository.AddBookAsync(book);
        }

        public async Task<BookDTO> GetBookByIdAsync(int bookId)
        {
            if (_cacheManager.IsSet(CacheKeys.GetBookKey(bookId)))
            {
                return _mapper.Map<BookDTO>(_cacheManager.Get<Book>(CacheKeys.GetBookKey(bookId)));
            }

            if (bookId <= 0)
            {
                throw new ArgumentException("id не может быть равен или меньше нуля");                
            }

            var book = await _bookRepository.GetBookByIdAsync(bookId);

            if(book == null)
            {
                throw new NotFoundException(nameof(Book), book);
            }

            return _mapper.Map<BookDTO>(book);           
        }

        public async Task<IEnumerable<BookDTO>> GetBooks(int skip , int take)
        {
            if (skip >= 0 && take >= 1)
            {
                return _mapper.Map<IEnumerable<BookDTO>>(await _bookRepository.GetBooks(skip, take));
            }
            return new List<BookDTO>();
        }

        public async Task RemoveBookAsync(int bookId)
        {
            if (bookId <= 0)
            {
                throw new ArgumentException("id не может быть равен или меньше нуля");
            }

            var book = await _bookRepository.GetBookByIdAsync(bookId);

            if (book != default)
            {
                await _bookRepository.RemoveBookAsync(book);
                _cacheManager.Remove(CacheKeys.GetBookKey(bookId));
            }
        }

        public async Task UpdateBookAsync(BookDTO bookDTO)
        {
            if(bookDTO == null)
            {
                throw new ArgumentNullException(nameof(BookDTO));
            }

            await _bookRepository.UpdateBookAsync(_mapper.Map<Book>(bookDTO));
            _cacheManager.Remove(CacheKeys.GetBookKey(bookDTO.Id));
        }

        public async Task<bool> IsBookInBasketAsync(int basketId, int bookId)
        {
            Book book = new Book();

            if ((book = await _bookRepository.GetBookByIdAsync(bookId)) != null && 
                (book.BookBaskets.FirstOrDefault(p => p.BookId == bookId && basketId == p.BasketId)) != default)
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<BookDTO>> GetBooksByCategoryAsync(int skip, int take, int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("id не может быть равен или меньше нуля");
            }

            Category category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                throw new ArgumentNullException(nameof(Category));
            }

            return _mapper.Map<IEnumerable<BookDTO>>(await _bookRepository.GetBooks(skip, take, (b) => b.CategoryId == categoryId));
        }

        public async Task<int> GetCountBooks()
        {
            return await _bookRepository.GetCountBooks();
        }

        public async Task<IEnumerable<BookDTO>> GetBooksByNameAsync(int skip, int take, string bookName)
        {
            bookName = bookName.ToLower().Replace(" ", "");

            var a =  _mapper.Map<IEnumerable<BookDTO>>(await _bookRepository.GetBooks(skip, take, (b) => 
            b.Title.ToLower().Replace(" ", "") == bookName));

            return a;
        }
    }
}
