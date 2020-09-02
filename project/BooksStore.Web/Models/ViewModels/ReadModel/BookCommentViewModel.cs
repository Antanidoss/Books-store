using BooksStore.Web.Models.ViewModels.Index;

namespace BooksStore.Web.Models.ViewModels
{
    public class BookCommentViewModel
    {
        public string BookName { get; set; }
        public int BookId { get; set; }
        public bool UserIsComment { get; set; }
        public IndexViewModel<CommentViewModel> IndexCommentModel { get; set; }
    }
}
