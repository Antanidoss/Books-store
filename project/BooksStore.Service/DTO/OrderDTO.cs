using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Services.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public IEnumerable<BookDTO> OrderBooks { get; set; }
        public DateTime TimeOfCreate { get; set; }
        public DateTime TimeOfDelivery { get; set; }
    }
}
