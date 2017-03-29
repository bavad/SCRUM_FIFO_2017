using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FIFO.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Email не может быть пустым")]
        [MinLength(5, ErrorMessage = "Email слишком короткий")]
        [MaxLength(30, ErrorMessage = "Email не может содержать больше 30 символов")]
        [RegularExpression(@"(?i)\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", ErrorMessage = "Email имеет не верный формат")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Логин не может быть пустым")]
        [MinLength(3, ErrorMessage = "Логин слишком короткий")]
        [MaxLength(30, ErrorMessage = "Логин не может содержать больше 30 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Пароль не может быть пустым")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Пароль слишком короткий")]
        [MaxLength(30, ErrorMessage = "Пароль не может содержать больше 30 символов")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}