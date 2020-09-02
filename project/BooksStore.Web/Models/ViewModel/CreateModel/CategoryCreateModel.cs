using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Web.Models.ViewModel.CreateModel
{
    public class CategoryCreateModel
    {
        [Required(ErrorMessage = "Введите имя категории")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
