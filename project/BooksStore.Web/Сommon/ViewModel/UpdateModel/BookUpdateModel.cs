using System.ComponentModel.DataAnnotations;

namespace BooksStore.Web.Сommon.ViewModel.UpdateModel
{
    public class BookUpdateModel
    { 
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите названия книги")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Названия может быть от 3 до 50 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите описания книги")]
        [StringLength(1500, MinimumLength = 20, ErrorMessage = "Описания может быть от 20 до 400 символов")]
        public string Descriptions { get; set; }

        [Required(ErrorMessage = "Введите цену книги")]
        [Range(50, 100000, ErrorMessage = "Цена может быть от 5 до 1000000 рублей")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Введите количество страниц книги")]
        [Range(5, 4000, ErrorMessage = "Количесво страниц может быть от 5 до 4000")]
        public int NumberOfPages { get; set; }
        public bool InStock { get; set; }
        public string CategoryName { get; set; }
        public string ImgPath { get; set; }
    }
}
