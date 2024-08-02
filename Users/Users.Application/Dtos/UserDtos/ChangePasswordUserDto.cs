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
    public class ChangePasswordUserDto
    {

        public int UserId { get; set; }

        [Display(Name = "پسورد قدیمی")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        [Length(5, 8, ErrorMessage = ErrorMessages.PasswordLengthError)]
        [PasswordValidation]
        public string OldPassword { get; set; }

        [Display(Name = "پسورد جدید")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        [Length(5, 8, ErrorMessage = ErrorMessages.PasswordLengthError)]
        [PasswordValidation]
        public string NewPassword { get; set; }

        [Display(Name = " تکرار پسورد جدید")]
        [Compare(nameof(NewPassword), ErrorMessage = ErrorMessages.PasswordConfirmError)]
        public string ConfirmPassword { get; set; }

    }
}
