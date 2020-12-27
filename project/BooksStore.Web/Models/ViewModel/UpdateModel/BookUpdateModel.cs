namespace BooksStore.Web.Models.ViewModel.UpdateModel
{
    public class BookUpdateModel
    { 
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPages { get; set; }
        public bool InStock { get; set; }
        public string CategoryName { get; set; }
        public string ImgPath { get; set; }
    }
}
