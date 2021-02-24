using System.ComponentModel.DataAnnotations;

namespace BooksStore.Web.Сommon.ViewModel.CreateModel
{
    public class RoleCreateModel
    {
        [Required(ErrorMessage = "Введите названия")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Длина имени должна быть от 4 до 50 символов")]
        public string Name { get; set; }
    }
}
