using BooksStore.Core.Entities;
using QueryableFilterSpecification.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Infastructure.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Task RemoveAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task<IEnumerable<Comment>> GetAsync(int skip, int take, IQueryableFilterSpec<Comment> filter);
        Task<Comment> GetAsync(IQueryableFilterSpec<Comment> filter);
        Task<int> GetCountAsync();
    }
}