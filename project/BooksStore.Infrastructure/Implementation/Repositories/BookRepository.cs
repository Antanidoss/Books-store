using BooksStore.Core.Entities;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces.Repositories;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using QueryableFilterSpecification.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

            updateBook.UpdateTime = DateTime.Now;

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

        public async Task<IEnumerable<Book>> GetByFilterAsync(int skip, int take, IQueryableFilterSpec<Book> filter)
        {
            return await filter.ApplyFilter(_context.Books)
                .Include(p => p.Category)
                .Include(p => p.Img)
                .Include(p => p.Author)
                .AsExpandable()
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Books.CountAsync();
        }
    }
}