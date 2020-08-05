using BooksStore.Core.BookModel;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.BookRep
{
    public class BookRepository : IBookRepository
    {
        EFDbContext context { get; set; }
        public BookRepository(EFDbContext context) => this.context = context;

        public async Task AddBookAsync(Book book)
        {
            if(book != null && book != default)
            {
                context.Books.Add(book);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            var book = await context.Books
                .Include(p => p.BookBaskets)
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.Img)
                .FirstOrDefaultAsync(p => p.Id == bookId);

            return book != default ? book : null;
        }

        public async Task RemoveBookAsync(Book book)
        {
            context.Books.Remove(book);
            await context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            if(book != null && book != default)
            {
                context.Books.Update(book);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetBooks(int skip, int take)
        {
            return await context.Books
                .Skip(skip)
                .Take(take)
                .Include(p => p.Category)
                .Include(p => p.Img)
                .Include(p => p.Author)
                .ToListAsync();
        }

        public async Task<int> GetCountBooks()
        {
            return await context.Books.CountAsync();
        }
    }
}
