using BooksStore.Web.Models.ViewModels.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.ViewModels.Comment
{
    public class BookCommentViewModel
    {
        public string BookName { get; set; }
        public int BookId { get; set; }
        public bool UserIsComment { get; set; }
        public IndexViewModel<CommentViewModel> IndexCommentModel { get; set; }
    }
}
