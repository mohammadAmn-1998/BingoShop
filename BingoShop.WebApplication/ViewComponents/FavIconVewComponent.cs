using Microsoft.AspNetCore.Mvc;
using Site.Application.Contract.SiteSettingApplication.Query;

namespace BingoShop.WebApplication.ViewComponents
{
	public class FavIconViewComponent : ViewComponent
	{

		private readonly ISiteSettingQuery _settingQuery;

		public FavIconViewComponent(ISiteSettingQuery settingQuery)
		{
			_settingQuery = settingQuery;
		}

		public IViewComponentResult Invoke()
		{

			var model = _settingQuery.GetFavIconForUi();

			return View(model);

		}

	}
}
