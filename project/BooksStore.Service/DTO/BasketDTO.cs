using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Service.DTO
{
    public class BasketDTO
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public List<BookDTO> BasketBooks { get; set; }
    }
}
