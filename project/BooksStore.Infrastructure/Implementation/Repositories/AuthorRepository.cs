using BooksStore.Core.Entities;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using QueryableFilterSpecification.Interfaces;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Author>> GetAsync(int skip, int take, IQueryableFilterSpec<Author> filter)
        {
            return await filter.ApplyFilter(_context.Authors.AsNoTracking())
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<Author> GetAsync(IQueryableFilterSpec<Author> filter)
        {
            return await _context.Authors.AsNoTracking().FirstOrDefaultAsync(filter.ToExpression());
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Authors.CountAsync();
        }
    }
}