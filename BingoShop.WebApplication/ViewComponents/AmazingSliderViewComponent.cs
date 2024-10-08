using Microsoft.AspNetCore.Mvc;

namespace BingoShop.WebApplication.ViewComponents;

public class AmazingSliderViewComponent : ViewComponent
{
	public AmazingSliderViewComponent()
	{
	}
	public IViewComponentResult Invoke()
	{
		return View();
	}
}