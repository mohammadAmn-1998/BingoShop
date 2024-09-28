
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Shared.Application.Utility;

namespace PostModule.Application.Contract.UserPostApplication.Command;

public class CreatePackage
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
    [MaxLength(355 , ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Title { get;set; }
    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
    public string Description { get;set; }
    [Display(Name = "تعداد درخواست")]
    public int Count { get;set; }
    [Display(Name = "قیمت")]
    public int Price { get;set; }
    [Display(Name = "تصویر")]
    public IFormFile? ImageFile { get; set; }
    [Display(Name = "alt تصویر")]
    [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
    [MaxLength(150, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string ImageAlt { get; set; }
}
