using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.SiteSettingApplication.Query;

namespace BingoShop.WebApplication.ViewComponents;

public class BlogLogoHeaderViewComponent : ViewComponent
{

	private readonly ISiteSettingQuery _siteSettingQuery;

	public BlogLogoHeaderViewComponent(ISiteSettingQuery siteSettingQuery)
	{
		_siteSettingQuery = siteSettingQuery;
	}


	public IViewComponentResult Invoke()
	{

		return View(_siteSettingQuery.GetLogoForUi());

	}
}