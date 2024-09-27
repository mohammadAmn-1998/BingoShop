using Microsoft.AspNetCore.Http;
using Shared.Application;
using Shared.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Shared.Application.Utility;

namespace Site.Application.Contract.BanerApplication.Command
{
    public class CreateBaner
    {
        [Display(Name = "تصویر")]
        public IFormFile? ImageFile { get; set; }
        [Display(Name = "Alt تصویر")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        [MaxLength(250,ErrorMessage = ErrorMessages.MaxLengthError)]
        public string ImageAlt { get; set; }
        [Display(Name = "لینک مقصد")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        [MaxLength(900, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Url { get; set; }
        [Display(Name = "جایگاه")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public BanerState State { get; set; }

        public string? ImageName { get; set; }
    }
}
