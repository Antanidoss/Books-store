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
    internal class AuthorRepository : IAuthorRepository
    {
        private readonly EFDbContext _context;
        public AuthorRepository(EFDbContext context) => _context = context;
        public async Task AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(p => p.Id == id);
            return author != default ? author : null;
        }

        public async Task RemoveAsync(Author author)
        {
            _context.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAsync(int skip, int take)
        {
            return await _context.Authors
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAsync(int skip, int take, Expression<Func<Author, bool>> condition)
        {
            return await _context.Authors
                .AsExpandable()
                .Where(condition)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Authors.CountAsync();
        }
    }
}