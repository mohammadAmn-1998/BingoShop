using Shared.Application;
using System.ComponentModel.DataAnnotations;
using Shared.Application.Utility;

namespace PostModule.Application.Contract.PostApplication
{
    public class CreatePost
    {
        [Display(Name = "عنوان پست")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        [MaxLength(255 ,ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Title { get; set; }
        [Display(Name = "توضیحات تحویل")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public string Status { get; set; }
        [Display(Name = "اضافه بار هر کیلوگرم درون شهری تهران (تومان)")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public int TehranPricePlus { get; set; }
        [Display(Name = "اضافه بار هر کیلوگرم درون شهری مراکز استان ها (تومان)")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public int StateCenterPricePlus { get; set; }
        [Display(Name = "اضافه بار هر کیلوگرم درون شهری شهرستان ها (تومان)")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public int CityPricePlus { get; set; }
        [Display(Name = "اضافه بار هر کیلوگرم درون استانی (تومان)")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public int InsideStatePricePlus { get; set; }
        [Display(Name = "اضافه بار هر کیلوگرم برون استانی هم جوار (تومان)")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public int StateClosePricePlus { get; set; }
        [Display(Name = "اضافه بار هر کیلوگرم برون استانی غیر هم جوار (تومان)")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public int StateNonClosePricePlus { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public string Description { get; set; }
    }
}
