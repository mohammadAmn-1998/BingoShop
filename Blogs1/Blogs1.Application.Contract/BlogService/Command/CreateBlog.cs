using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Application.Utility;

namespace Blogs1.Application.Contract.BlogService.Command;

public class CreateBlog
{
	[Display(Name = "عنوان")]
	[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
	[MaxLength(1000,ErrorMessage = ErrorMessages.MaxLengthError)]
	
	public string Title { get;  set; }

	[Display(Name = "لینک سربرگ")]
	[MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
	[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
	public string Slug { get; set; }

	[Display(Name = "تصویر")]
	public string? ImageName { get;  set; }

	public IFormFile? ImageFile { get; set; }
	
	[Display(Name = "متن جایگزین تصویر")]
	[MaxLength(100, ErrorMessage = ErrorMessages.MaxLengthError)]
	[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
	public string ImageAlt { get;  set; }

	public long UserId { get;  set; }

	[Display(Name = "نام نویسنده")]
	[MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
	[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
	public string Author { get;  set; }

	[Display(Name = "دسته بندی")]
	public long CategoryId { get;  set; }

	[Display(Name = "زیر گروه")]
	public long SubCategoryId { get;  set; }

	[MaxLength(1000, ErrorMessage = ErrorMessages.MaxLengthError)]
	[Display(Name = "خلاصه")]
	[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
	public string Summary { get;  set; }

	[Display(Name = "محتوای پست")]
	[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
	public string Content { get;  set; }

	[Display(Name = "پست ویژه؟")]
	public bool IsSpecial { get;  set; }

	
	public List<SelectListItem>? Categories { get; set; }

	public List<SelectListItem>? SubCategories { get; set; }
}