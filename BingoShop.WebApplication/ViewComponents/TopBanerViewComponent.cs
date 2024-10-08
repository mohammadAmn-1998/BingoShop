using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enums;
using Site.Application.Contract.BanerApplication.Query;

namespace BingoShop.WebApplication.ViewComponents;

public class TopBanerViewComponent : ViewComponent
{
	private readonly IBanerQuery _banerQuery;

	public TopBanerViewComponent(IBanerQuery banerQuery)
	{
		_banerQuery = banerQuery;
	}

	public IViewComponentResult Invoke()
	{

		var model = _banerQuery.GetForUi(1, BanerState.بنر_تکی_باریک_بالا_1645x105);

		return View(model);

	}

}