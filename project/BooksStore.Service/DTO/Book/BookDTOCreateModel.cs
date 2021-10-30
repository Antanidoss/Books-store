namespace BooksStore.Services.DTO.Book
{
    public class BookDTOCreateModel
    {
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPages { get; set; }
        public bool InStock { get; set; }
        public string ImgPath { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}
