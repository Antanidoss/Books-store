using System.ComponentModel.DataAnnotations;

namespace BooksStore.Web.Models.ViewModel.UpdateModel
{
    public class CategoryUpdateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя категории")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
