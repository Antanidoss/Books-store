using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Service.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public IEnumerable<BookDTO> BooksOrder { get; set; }
        public DateTime TimeOfCreate { get; set; }
        public DateTime TimeOfDelivery { get; set; }
    }
}
