namespace BooksStore.Web.Сommon.ViewModel.ReadModel
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPages { get; set; }
        public bool InStock { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string AuthorFullName { get; set; }
        public int AuthorId { get; set; }
        public byte[] ImageData { get; set; }
        public bool IsAddToBasket { get; set; }
    }
}
