using BooksStore.Core.AuthorModel;
using BooksStore.Core.BookModel;
using BooksStore.Core.CategoryModel;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Service.BookSer
{
    public class BookService : IBookService
    {
        IBookRepository BookRepository { get; set; }
        ICategoryRepository CategoryRepository { get; set; }
        IAuthorRepository AuthorRepository { get; set; }
        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository, IAuthorRepository authorRepository)
        {
            BookRepository = bookRepository;
            CategoryRepository = categoryRepository;
            AuthorRepository = authorRepository;
        }

        public async Task AddBookAsync(Book book)
        {
            if(book != null && book != default)
            {
                Category category = await CategoryRepository.GetCategoryByName(book.Category.Name);
                if (category != null)
                {                
                    book.Category = category;
                }

                Author author = await AuthorRepository.GetAuthorByName(book.Author.FirstName, book.Author.Surname);
                if (author != null)
                {
                    book.Author = author;
                }

                await BookRepository.AddBookAsync(book);
            }
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            if (bookId >= 1)
            {
                return await BookRepository.GetBookByIdAsync(bookId);
            }
            return null;
        }

        public async Task<IEnumerable<Book>> GetBooks(int skip , int take)
        {
            if (skip >= 0 && take >= 1)
            {
                return await BookRepository.GetBooks(skip, take);
            }
            return new List<Book>();
        }

        public async Task RemoveBookAsync(int bookId)
        {
            if (bookId >= 1)
            {
                var book = await BookRepository.GetBookByIdAsync(bookId);

                if (book != default)
                {
                    await BookRepository.RemoveBookAsync(book);
                }
            }
        }

        public async Task UpdateBookAsync(Book book)
        {
            if(book != null && book != default)
            {
                await BookRepository.UpdateBookAsync(book);
            }
        }

        public async Task<bool> IsBookInBasketAsync(int basketId, int bookId)
        {
            Book book = new Book();

            if ((book = await BookRepository.GetBookByIdAsync(bookId)) != null && 
                (book.BookBaskets.FirstOrDefault(p => p.BookId == bookId && basketId == p.BasketId)) != default)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Book>> GetBookByCategoryAsync(int categoryId)
        {
            if (categoryId >= 1)
            {
                Category category = await CategoryRepository.GetCategoryByIdAsync(categoryId);
                if (category != null)
                {
                    return (await BookRepository.GetBooks(0, 6)).Where(p => p.CategoryId == categoryId);
                }
            }
            return new List<Book>();
        }

        public async Task<int> GetCountBooks()
        {
            return await BookRepository.GetCountBooks();
        }
    }
}
