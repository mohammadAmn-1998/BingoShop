using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.SiteSettingApplication.Query;

namespace BingoShop.WebApplication.ViewComponents
{
	public class LogoHeaderViewComponent : ViewComponent
	{

		private readonly ISiteSettingQuery _settingQuery;

		public LogoHeaderViewComponent(ISiteSettingQuery settingQuery)
		{
			_settingQuery = settingQuery;
		}

		public IViewComponentResult Invoke()
		{

			var model = _settingQuery.GetLogoForUi();
			return View(model);

		}

	}
}
