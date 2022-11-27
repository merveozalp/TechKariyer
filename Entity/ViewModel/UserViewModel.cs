using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class UserViewModel
    {
        [Display(Name ="Email Adres")]
        [Required(ErrorMessage ="Email Alanı Boş Geçilemez")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name ="Şifre")]
        [Required(ErrorMessage ="Şifre Boş Geçilemez")]
        [DataType(DataType.Password)]
        [MinLength(4,ErrorMessage ="Şifre en az 4 Karakterli olmalıdır.")]
        public string Password { get; set; }
    }
}
