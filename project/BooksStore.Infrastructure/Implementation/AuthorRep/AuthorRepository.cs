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
        EFDbContext context { get; set; }
        public AuthorRepository(EFDbContext context) => this.context = context;
        public async Task AddAuthorAsync(Author author)
        {
            context.Authors.Add(author);
            await context.SaveChangesAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            var author = await context.Authors.FirstOrDefaultAsync(p => p.Id == id);
            return author != default ? author : null;
        }       

        public async Task RemoveAuthorAsync(Author author)
        {
            context.Remove(author);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            context.Update(author);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync(int skip, int take)
        {
            return await context.Authors.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthors(int skip, int take)
        {
            return await context.Authors
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<Author> GetAuthorByName(string firstName, string surname)
        {
            var author = await context.Authors
                .FirstOrDefaultAsync(p => p.Firstname == firstName);

            return author != default ? author : null;
        }

        public async Task<int> GetCountAuthors()
        {
            return await context.Authors.CountAsync();
        }
    }
}
