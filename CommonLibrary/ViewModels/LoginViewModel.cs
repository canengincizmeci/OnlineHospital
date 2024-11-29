using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {

        }
        public LoginViewModel(string email, string password)
        {
            Email = email;
            Password = password;
            
        }

        [EmailAddress(ErrorMessage = "Email formatı yanlış")]
        [Required(ErrorMessage = "Email alanı boş bırakılamaz")]
        [Display(Name = "Email :")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz")]
        [Display(Name = "Şifre:")]
        public string Password { get; set; }

       
    }
}
