using BooksStore.Core.Entities;
using BooksStore.Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetAsync(int skip, int take, int bookId);
        Task<int> GetCountAsync();
    }
}
