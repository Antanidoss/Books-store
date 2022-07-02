using BooksStore.Core.Entities;
using BooksStore.Infastructure.Data;
using BooksStore.Services.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using QueryableFilterSpecification.Interfaces;
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
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAsync(int skip, int take)
        {
            return await _context.Books
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .Include(p => p.Category)
                .Include(p => p.Img)
                .Include(p => p.Author)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAsync(int skip, int take, IQueryableFilterSpec<Book> filter)
        {
            return await filter.ApplyFilter(_context.Books.AsNoTracking())
                .Include(p => p.Category)
                .Include(p => p.Img)
                .Include(p => p.Author)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<Book> GetAsync(IQueryableFilterSpec<Book> filter)
        {
            return await _context.Books
                .Include(b => b.BookBaskets)
                .Include(p => p.Category)
                .Include(p => p.Img)
                .Include(p => p.Author)
                .FirstOrDefaultAsync(filter.ToExpression());
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Books.CountAsync();
        }
    }
}