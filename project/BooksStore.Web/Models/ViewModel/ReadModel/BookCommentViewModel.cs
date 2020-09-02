using BooksStore.Web.Models.ViewModel.Index;

namespace BooksStore.Web.Models.ViewModel.ReadModel
{
    public class BookCommentViewModel
    {
        public string BookName { get; set; }
        public int BookId { get; set; }
        public bool UserIsComment { get; set; }
        public IndexViewModel<CommentViewModel> IndexCommentModel { get; set; }
    }
}
