using BooksStore.Core.AuthorModel;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.AuthorRep
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly EFDbContext _context;
        public AuthorRepository(EFDbContext context) => this._context = context;
        public async Task AddAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(p => p.Id == id);
            return author != default ? author : null;
        }       

        public async Task RemoveAuthorAsync(Author author)
        {
            _context.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            _context.Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync(int skip, int take)
        {
            return await _context.Authors.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthors(int skip, int take)
        {
            return await _context.Authors
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<Author> GetAuthorByName(string firstName, string surname)
        {
            var author = await _context.Authors
                .FirstOrDefaultAsync(p => p.Firstname == firstName);

            return author != default ? author : null;
        }

        public async Task<int> GetCountAuthors()
        {
            return await _context.Authors.CountAsync();
        }
    }
}
