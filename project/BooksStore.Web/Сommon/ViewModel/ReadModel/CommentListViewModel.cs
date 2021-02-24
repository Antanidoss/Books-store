using BooksStore.Web.Сommon.ViewModel.Index;
using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Web.Сommon.ViewModel.ReadModel
{
    public class CommentListViewModel
    {
        public string BookName { get; set; }
        public int BookId { get; set; }
        public bool UserIsComment { get; set; }
        public IndexViewModel<CommentViewModel> IndexCommentModel { get; set; }

        public CommentListViewModel(string bookName,int bookId, bool userIsComment, int pageNum, int pageSize, int totalItems, 
            IEnumerable<CommentViewModel> comments)
        {
            BookName = bookName;
            BookId = bookId;
            UserIsComment = userIsComment;
            IndexCommentModel = new IndexViewModel<CommentViewModel>(pageNum, pageSize, totalItems,
                comments.Skip((pageNum - 1) * pageSize).Take(pageSize));
        }
    }
}
