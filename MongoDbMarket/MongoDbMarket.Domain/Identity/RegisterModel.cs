using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbMarket.Domain.Identity
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Имя")]
        public String Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Электронный адресс")]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public String Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Пароль еще раз")]
        public String ConfirmPassword { get; set; }

        [Display(Name = "Страна")]
        public String Country { get; set; }

        [Display(Name = "Телефон")]
        public String PhoneNumber { get; set; }

        [Display(Name = "Skype")]
        public String Skype { get; set; }
    }
}
