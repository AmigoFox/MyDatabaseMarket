using System.ComponentModel.DataAnnotations;

namespace MyDatabaseMarket.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите Email")]
        [EmailAddress(ErrorMessage = "Неверный формат Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов")]
        public string Password { get; set; } = string.Empty;
    }
}
