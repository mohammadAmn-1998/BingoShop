using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Shared.Application.Utility;

namespace Blogs1.Application.Contract.BlogCategoryService.Command
{
	public class CreateBlogCategory
	{
		[Display(Name = "عنوان")]
		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
		[MaxLength(1000,ErrorMessage = ErrorMessages.MaxLengthError)]
		public string Title { get;  set; }

		[Display(Name = "تصویر")]
		public IFormFile? ImageFile { get; set; }

		[Display(Name = "تصویر")]
		public string? ImageName { get; set; }

		[Display(Name = "متن تصویر")]
		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
		[MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
		public string ImageAlt { get;  set; }

		public long ParentId { get; set; }

		[Display(Name = "اسلاگ")]
		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
		[MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
		public string Slug { get; set; }

	}
}