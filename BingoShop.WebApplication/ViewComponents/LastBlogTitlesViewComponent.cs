using Blogs1.Application.Contract.BlogService.Query;
using Microsoft.AspNetCore.Mvc;

namespace BingoShop.WebApplication.ViewComponents
{
	public class LastBlogTitlesViewComponent : ViewComponent
	{

		private readonly IBlogQuery _blogQuery;

		public LastBlogTitlesViewComponent(IBlogQuery blogQuery)
		{
			_blogQuery = blogQuery;
		}

		public IViewComponentResult Invoke()
		{

			return View(_blogQuery.GetBlogLastTitles());

		}


	}
}
