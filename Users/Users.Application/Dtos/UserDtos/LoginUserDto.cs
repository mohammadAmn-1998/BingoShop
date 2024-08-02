using Shared.Application.Utility.Validations;
using Shared.Application.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Dtos.UserDtos
{
    public class LoginUserDto
    {

        [Display(Name = "نام کاربری یا موبایل یا ایمیل")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public string UserName { get; set; }

        [Display(Name = "پسورد")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        [Length(5, 8, ErrorMessage = ErrorMessages.PasswordLengthError)]
        [PasswordValidation]
        public string Password { get; set; }


    }
}
