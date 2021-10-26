using BooksStore.Services.DTO.Book;
using System.Collections.Generic;

namespace BooksStore.Services.DTO.Basket
{
    public class BasketDTO
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public List<BookDTO> BasketBooks { get; set; }
    }
}