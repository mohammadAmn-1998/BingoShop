using Microsoft.AspNetCore.Http;
using Shared.Application;
using System.ComponentModel.DataAnnotations;
using Shared.Application.Utility;

namespace Site.Application.Contract.ImageSiteApplication.Command
{
	public class CreateImageSite
	{
		[Display(Name = "تصویر")]
		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
		public IFormFile ImageFile { get; set; }
		[Display(Name = "عنوان")]
		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
		[MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
		public string Title { get; set; }

		public string? ImageName { get; set; }
	}
}
