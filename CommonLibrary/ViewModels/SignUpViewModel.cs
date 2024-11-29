using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.ViewModels
{
    public class SignUpViewModel
    {
        public SignUpViewModel()
        {

        }

        public SignUpViewModel(string userName, string userEmail, string password, string passwordConfirm)
        {
            UserName = userName;
            UserEmail = userEmail;
            Password = password;
            PasswordConfirm = passwordConfirm;
        }

        [Required(ErrorMessage = "Kullanıcı ad alanı boş bırakılamaz")]
        [Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; set; } = null!;


        [EmailAddress(ErrorMessage = "Email formatı yanlış")]
        [Required(ErrorMessage = "Email alanı boş bırakılamaz")]
        [Display(Name = "Email :")]
        public string UserEmail { get; set; } = null!;


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Telefon alanı boş bırakılamaz")]
        [Display(Name = "Şifre :")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olabilir.")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Şifre aynı değildir.")]
        [Required(ErrorMessage = "Şifre Tekrar alanı boş bırakılamaz")]
        [Display(Name = "Şifre Tekrar:")]
        public string PasswordConfirm { get; set; } = null!;


    }
}
