using BooksStore.Core.Entities;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces.Repositories;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Implementation.Repositories
{
    internal class BookRepository : IBookRepository
    {
        private readonly EFDbContext _context;
        public BookRepository(EFDbContext context) => _context = context;

        public async Task AddAsync(Book book)
        {
            if (book != null && book != default)
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Book> GetByIdAsync(int bookId)
        {
            var book = await _context.Books
                .Include(p => p.BookBaskets)
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.Img)
                .FirstOrDefaultAsync(p => p.Id == bookId);

            return book != default ? book : null;
        }

        public async Task RemoveAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            var updateBook = await _context.Books.FirstOrDefaultAsync(p => p.Id == book.Id);

            updateBook.Title = book.Title;
            updateBook.Descriptions = book.Descriptions;
            updateBook.Price = book.Price;
            updateBook.UpdateTime = DateTime.Now;
            updateBook.NumberOfPages = book.NumberOfPages;
            updateBook.InStock = book.InStock;

            _context.Books.Update(updateBook);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAsync(int skip, int take)
        {
            return await _context.Books
                .Skip(skip)
                .Take(take)
                .Include(p => p.Category)
                .Include(p => p.Img)
                .Include(p => p.Author)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAsync(int skip, int take, Expression<Func<Book, bool>> func)
        {
            return _context.Books
                .Include(p => p.Category)
                .Include(p => p.Img)
                .Include(p => p.Author)
                .AsExpandable()
                .Where(func)
                .Skip(skip)
                .Take(take);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Books.CountAsync();
        }
    }
}