using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enums;
using Site.Application.Contract.BanerApplication.Query;

namespace BingoShop.WebApplication.ViewComponents;

public class BottomBanerViewComponent : ViewComponent
{
	private readonly IBanerQuery _banerQuery;

	public BottomBanerViewComponent(IBanerQuery banerQuery)
	{
		_banerQuery = banerQuery;
	}

	public IViewComponentResult Invoke()
	{

		var model = _banerQuery.GetForUi(1, BanerState.بنر_تکی_پایین_1680x210);

		return View(model);

	}

}