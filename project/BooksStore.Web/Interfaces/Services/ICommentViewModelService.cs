using BooksStore.Web.Сommon.ViewModel.CreateModel;
using BooksStore.Web.Сommon.ViewModel.ReadModel;
using BooksStore.Web.Сommon.ViewModel.UpdateModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksStore.Web.Interfaces.Services
{
    public interface ICommentViewModelService
    {
        Task AddCommentAsync(CommentCreateModel model);
        Task<IEnumerable<CommentViewModel>> GetCommentsAsync(int pageNum, int bookId);
        Task RemoveCommentAsync(int commentId);
    }
}
