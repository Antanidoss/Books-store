using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace BooksStore.Web.Models.ViewModel.CreateModel
{
    public class BookCreateModel
    {
        [Required(ErrorMessage = "Введите названия книги")]
        [StringLength(100 , MinimumLength = 3 , ErrorMessage = "Названия может быть от 3 до 50 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите описания книги")]
        [StringLength(1500, MinimumLength = 200, ErrorMessage = "Описания может быть от 20 до 400 символов")]
        public string Descriptions { get; set; }

        [Range(1 , 600)]
        public decimal Price { get; set; }

        [Range(1, 3000)]
        public int NumberOfPages { get; set; }
        public bool InStock { get; set; }

        [Required(ErrorMessage = "Введите названия категории книги")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Введите имя автора книги")]
        public string AuthorFirstname { get; set; }

        [Required(ErrorMessage = "Введите фамилию автора книги")]
        public string AuthorSurname { get; set; }

        [Required(ErrorMessage = "Выберите изображения")]
        public IFormFile ImgFile { get; set; }

        public string ImgPath { get; set; }
    }
}
