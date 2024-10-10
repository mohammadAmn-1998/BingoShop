using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enums;
using Site.Application.Contract.BanerApplication.Query;

namespace BingoShop.WebApplication.ViewComponents;

	public class BlogLeftBanerViewComponent : ViewComponent
	{

		private readonly IBanerQuery _banerQuery;

		public BlogLeftBanerViewComponent(IBanerQuery banerQuery)
		{
			_banerQuery = banerQuery;
		}


		public IViewComponentResult Invoke()
		{

			return View(_banerQuery.GetForUi(1,BanerState.بنر_تکی_وبلاگ_280x230));

		}
	}


public class BlogMidBanerViewComponent : ViewComponent
{

	private readonly IBanerQuery _banerQuery;

	public BlogMidBanerViewComponent(IBanerQuery banerQuery)
	{
		_banerQuery = banerQuery;
	}


	public IViewComponentResult Invoke()
	{

		return View(_banerQuery.GetForUi(1, BanerState.بنر_تکی_وبلاگ_1020x130));

	}
}

