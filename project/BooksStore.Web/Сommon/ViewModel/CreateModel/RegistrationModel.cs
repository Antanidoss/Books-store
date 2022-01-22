using System.ComponentModel.DataAnnotations;

namespace BooksStore.Web.Сommon.ViewModel.CreateModel
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Введите имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина имени должна быть от 3 до 50 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите эл.почту")]
        [StringLength(254, MinimumLength = 3, ErrorMessage = "Длина эл.почты должна быть от 3 до 254 символов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите подтверждение пароля")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Длина подтвержения пароля должна быть от 10 до 100 символов")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Длина пароля должна быть от 10 до 100 символов")]
        public string Password { get; set; }
        public bool IsParsistent { get; set; }
    }
}
