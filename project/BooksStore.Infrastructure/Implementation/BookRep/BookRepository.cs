using BooksStore.Core.BookModel;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.BookRep
{
    public class BookRepository : IBookRepository
    {
        private readonly EFDbContext _context;
        public BookRepository(EFDbContext context) => this._context = context;

        public async Task AddBookAsync(Book book)
        {
            if(book != null && book != default)
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            var book = await _context.Books
                .Include(p => p.BookBaskets)
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.Img)
                .FirstOrDefaultAsync(p => p.Id == bookId);

            return book != default ? book : null;
        }

        public async Task RemoveBookAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            if(book != null && book != default)
            {
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetBooks(int skip, int take)
        {
            return await _context.Books
                .Skip(skip)
                .Take(take)
                .Include(p => p.Category)
                .Include(p => p.Img)
                .Include(p => p.Author)
                .ToListAsync();
        }

        public async Task<int> GetCountBooks()
        {
            return await _context.Books.CountAsync();
        }

        public async Task<IEnumerable<Book>> GetBooks(int skip, int take, Func<Book,bool> func)
        {
            return _context.Books             
                .Skip(skip)
                .Take(take)
                .Include(p => p.Category)
                .Include(p => p.Img)
                .Include(p => p.Author)
                .Where(func);
        }
    }
}
