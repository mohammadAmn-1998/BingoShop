using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;
using Shared.Application.Utility.Validations;

namespace Users.Application.Dtos.UserAddressDtos
{
    public class CreateUserAddressDto
    {


        public int StateId { get; set; }

        public int CityId { get; set; }

        [Display(Name = "آدرس کامل")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string AddressDetail { get; set; }

        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        [MaxLength(10, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string PostalCode { get; set; }

        [Display(Name = "تلفن همراه ")]
        [MaxLength(11, ErrorMessage = ErrorMessages.MaxLengthError)]
        [MobileValidation]
        public string Phone { get; set; }

        [Display(Name = "نام کامل ")]
        [MaxLength(255, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string FullName { get; set; }

        [Display(Name = "کد ملی (اختیاری) ")]
        [MaxLength(10, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? IranCode { get; set; }



    }
}
