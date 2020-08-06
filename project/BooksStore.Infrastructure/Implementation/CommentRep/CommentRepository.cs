using BooksStore.Core.BookModel;
using BooksStore.Core.CommentModel;
using BooksStore.Infastructure.Data;
using BooksStore.Infastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.CommentRep
{
    public class CommentRepository : ICommentRepository
    {
        EFDbContext context { get; set; }        
        public CommentRepository(EFDbContext context) => this.context = context;

        public IEnumerable<Comment> Comments => context.Comments.Include(p => p.AppUser).ToArray();

        public async Task AddCommentAsync(Comment comment)
        {
            context.Comments.Add(comment);
            await context.SaveChangesAsync();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            var comment = await context.Comments
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(p => p.Id == id);

            return comment != default ? comment : null;
        }
       
        public async Task RemoveCommentAsync(Comment comment)
        {
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            context.Comments.Update(comment);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetComments(int skip, int take)
        {
            return await context.Comments
                .Skip(skip)
                .Take(take)
                .Include(p => p.AppUser)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentByBookId(int bookId)
        {
            return await context.Comments.Where(p => p.BookId == bookId).ToListAsync();
        }

        public async Task<int> GetCountComments()
        {
            return await context.Comments.CountAsync();
        }
    }
}
