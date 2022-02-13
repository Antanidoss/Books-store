using System;

namespace BooksStore.Services.DTO.Book
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPages { get; set; }
        public bool InStock { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string AuthorFirstname { get; set; }
        public string AuthorSurname { get; set; }
        public int AuthorId { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
        public int ImgId { get; set; }
        public DateTime TimeOfCreate { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
