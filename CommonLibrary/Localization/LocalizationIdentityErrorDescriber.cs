using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Localization
{
    public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new() { Description = $"Bu kullanıcı adı \"{userName}\" alınmış", Code = "DuplicateUserName" };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Description = $"Bu email adresi \"{email}\" alınmış ", Code = "DuplicateEmail" };
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Description = "Şifre en az 6 karakterli olmalıdır.", Code = "PasswordTooShort" };
        }


    }
}
