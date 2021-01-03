using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.ViewModel.CreateModel
{
    public class CommentCreateModel
    {
        [Required(ErrorMessage = "Введите описания комметария")]
        [StringLength(400, MinimumLength = 5, ErrorMessage = "Описания может быть от 5 до 400 символов")]
        public string Descriptions { get; set; }
        public int BookId { get; set; }
    }
}
