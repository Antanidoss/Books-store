using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Service.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Descriptions { get; set; }
        public int BookId { get; set; }
        public DateTime TimeOfCreate { get; set; }
        public string AppUserName { get; set; }
        public string AppUserId { get; set; }
    }
}
