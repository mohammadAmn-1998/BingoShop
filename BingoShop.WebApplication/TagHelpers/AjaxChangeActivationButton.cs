using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace BingoShop.WebApplication.TagHelpers
{
	public class AjaxChangeActivationButton : TagHelper
	{
		public string URL { get; set; }

		public string ErrorTitle { get; set; } = "";

		public string ErrorText { get; set; } = "";

		public string BtnColor { get; set; } = "info";

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "a";
			output.Attributes.Add("onclick", $"changeActivation('{URL}','{ErrorTitle}','{ErrorText}')");
			//btn btn-danger mr - 1
			output.AddClass("btn", HtmlEncoder.Default);
			output.AddClass($"btn-{BtnColor}", HtmlEncoder.Default);
			output.AddClass("mr-1", HtmlEncoder.Default);
			
			base.Process(context, output);
		}
	}
}
