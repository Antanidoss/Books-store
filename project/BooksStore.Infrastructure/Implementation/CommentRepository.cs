using BooksStore.Core.Entities;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Infastructure
{
    public class CommentRepository : ICommentRepository
    {
        private readonly EFDbContext _context;        
        public CommentRepository(EFDbContext context) => this._context = context;

        public IEnumerable<Comment> Comments => _context.Comments.Include(p => p.AppUser).ToArray();

        public async Task AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            var comment = await _context.Comments
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(p => p.Id == id);

            return comment != default ? comment : null;
        }
       
        public async Task RemoveCommentAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetComments(int skip, int take)
        {
            return await _context.Comments
                .Skip(skip)
                .Take(take)
                .Include(p => p.AppUser)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentByBookId(int bookId)
        {
            return await _context.Comments.Where(p => p.BookId == bookId).ToListAsync();
        }

        public async Task<int> GetCountComments()
        {
            return await _context.Comments.CountAsync();
        }
    }
}
