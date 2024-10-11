﻿using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enums;
using Site.Application.Contract.BanerApplication.Query;

namespace BingoShop.WebApplication.ViewComponents;

public class ButtomBanerBlogViewComponent : ViewComponent
{
	private readonly IBanerQuery _query;
	public ButtomBanerBlogViewComponent(IBanerQuery query)
	{
		_query = query;
	}
	public IViewComponentResult Invoke()
	{
		var model = _query.GetForUi(1, BanerState.بنر_تکی_وبلاگ_1020x130);
		return View(model);
	}
}