using Shared.Application;
using System.ComponentModel.DataAnnotations;
using Shared.Application.Utility;

namespace PostModule.Application.Contract.PostPriceApplication
{
    public class UbsertPostPrice
    {
        [Display(Name = " وزن از (گرم) ")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public int Start { get; set; }
        [Display(Name = " وزن تا (گرم) ")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public int End { get; set; }
        [Display(Name = "قیمت درون شهری تهران ")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public int TehranPrice { get; set; }
        [Display(Name = "قیمت درون شهری مراکز استان ")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public int StateCenterPrice { get; set; }
        [Display(Name = "قیمت درون شهری شهرستان ها ")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public int CityPrice { get; set; }
        [Display(Name = "قیمت درون استانی ")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public int InsideStatePrice { get; set; }
        [Display(Name = "قیمت برون استانی همجوار ")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public int StateClosePrice { get; set; }
        [Display(Name = "قیمت برون استانی غیر همجوار ")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public int StateNonClosePrice { get; set; }
    }
}
