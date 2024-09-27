
using System.ComponentModel.DataAnnotations;
using Shared.Application.Utility;
using Shared.Application.Utility.BaseCommands;

namespace Site.Application.Contract.SitePageApplication.Command
{
	public class CreateSitePage : Title_Slug
	{
		[Display(Name = "توضیح")]
		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
		public string Text { get; set; }
	}
}
