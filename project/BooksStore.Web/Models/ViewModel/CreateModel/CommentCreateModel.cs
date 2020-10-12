using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.ViewModel.CreateModel
{
    public class CommentCreateModel
    {
        public string Descriptions { get; set; }
        public int BookId { get; set; }
    }
}
