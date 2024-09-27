using Microsoft.AspNetCore.Http;
using Shared.Application;
using System.ComponentModel.DataAnnotations;
using Shared.Application.Utility;

namespace Site.Application.Contract.SiteServiceApplication.Command
{
    public class CreateSiteService
    {
        [Display(Name = "تصویر")]
        public IFormFile? ImageFile { get; set; }
        [Display(Name = "Alt تصویر")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string ImageAlt { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        [MaxLength(450, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Title { get; set; }
    }
}
