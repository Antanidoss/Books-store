﻿using BooksStore.Core.Entities;
using BooksStore.Infastructure.Data;
using BooksStore.Services.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using QueryableFilterSpecification.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Implementation.Repositories
{
    internal class CommentRepository : ICommentRepository
    {
        private readonly EFDbContext _context;
        public CommentRepository(EFDbContext context) => _context = context;

        public IEnumerable<Comment> Comments => _context.Comments.Include(p => p.AppUser).ToArray();

        public async Task AddAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var comment = await _context.Comments
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(p => p.Id == id);

            return comment != default ? comment : null;
        }

        public async Task RemoveAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetAsync(int skip, int take, IQueryableFilterSpec<Comment> filter)
        {
            return await filter.ApplyFilter(_context.Comments.AsNoTracking())
                .Skip(skip)
                .Take(take)
                .Include(p => p.AppUser)
                .ToListAsync();

        }

        public async Task<Comment> GetAsync(IQueryableFilterSpec<Comment> filter)
        {
            return await _context.Comments.FirstOrDefaultAsync(filter.ToExpression());
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Comments.CountAsync();
        }
    }
}