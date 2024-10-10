
using Blogs1.Application.Contract.BlogService.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Models;

namespace BingoShop.WebApplication.Controllers
{
	public class BlogController : ControllerBase
	{
		private readonly IBlogQuery _blogQuery;

		public BlogController(IBlogQuery blogQuery)
		{
			_blogQuery = blogQuery;
		}

		public async Task<IActionResult> Index(int pageId=1)
		{
			return View( await _blogQuery.GetFilteredPosts(new FilterParams(pageId,6,"")));

		}

		[Route("/Blog/ChangePage/{pageId}")]
		public async Task<IActionResult> ChangePage(int pageId)
		{
			var model = await _blogQuery.GetFilteredPosts(new(pageId, 6, ""));
			return PartialView("Blog/_BlogNewsPartial",model);

		}
	}
}
