using Blogs1.Application.Contract.BlogService.Query;
using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.SiteSettingApplication.Query;

namespace BingoShop.WebApplication.ViewComponents;

public class SpecialBlogsViewComponent : ViewComponent
{

	private readonly IBlogQuery _blogQuery;

	public SpecialBlogsViewComponent(IBlogQuery blogQuery)
	{
		_blogQuery = blogQuery;
	}

	public IViewComponentResult Invoke()
	{

		return View(_blogQuery.GetSpecialBlogsForUI());

	}
}