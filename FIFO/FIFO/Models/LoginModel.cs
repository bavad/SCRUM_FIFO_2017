using System.ComponentModel.DataAnnotations;

namespace FIFO.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email не может быть пустым")]
        [EmailAddress(ErrorMessage = "Email имеет не верный формат")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль не может быть пустым")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}