using Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shared.Application.Utility;
using Shared.Application.Utility.Validations;

namespace Users.Application.Dtos.UserDtos
{
    public class EditUserByUserDto
    {
        public int Id { get; set; }

        [Display(Name = "نام")]
        [MaxLength(255, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [MaxLength(255, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? LastName { get; set; }

        [Display(Name = "نام کاربری")]
        [MaxLength(255, ErrorMessage = ErrorMessages.MaxLengthError)]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public string UserName { get; set; }

        [Display(Name = "تصویر نمایه")]
        public IFormFile? AvatarImageFile { get; set; }

        public string? avatarImagaName { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(255, ErrorMessage = ErrorMessages.MaxLengthError)]
        [EmailValidation(ErrorMessage = ErrorMessages.EmailIsInvalid)]
        public string? EmailAddress { get; set; }

        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        [MobileValidation(ErrorMessage = ErrorMessages.MobileIsInvalid)]
        public string Mobile { get; set; }

        [Display(Name = "بیوگرافی")]
        [MaxLength(1000, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Biography { get; set; }

        [Display(Name = "جنسیت")]
        public Gender Gender { get; set; } = Gender.نامشخص;


    }
}
