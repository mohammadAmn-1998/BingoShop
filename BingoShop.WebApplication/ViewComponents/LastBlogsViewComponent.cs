using Blogs1.Application.Contract.BlogService.Query;
using Microsoft.AspNetCore.Mvc;


namespace BingoShop.WebApplication.ViewComponents
{
	public class LastBlogsViewComponent : ViewComponent
	{

		private readonly IBlogQuery _blogQuery;

		public LastBlogsViewComponent(IBlogQuery blogQuery)
		{
			_blogQuery = blogQuery;
		}


		public IViewComponentResult Invoke()
		{

			return View(_blogQuery.GetLastBlogsForUI());

		}
	}
}
