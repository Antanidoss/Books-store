using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Descriptions { get; set; }
        public string AppUserName { get; set; }
        public string TimeCreate { get; set; } 

    }
}
