using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        public Task<IEnumerable<Author>> GetAsync(int skip, int take);
        public Task<Author> GetByNameAsync(string firstName, string surname);
        public Task<int> GetCountAsync();
    }
}