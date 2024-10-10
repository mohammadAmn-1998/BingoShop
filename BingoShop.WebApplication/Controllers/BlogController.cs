
using Blogs1.Application.Contract.BlogService.Query;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.Ui.Blog;
using Shared.Application.Models;

namespace BingoShop.WebApplication.Controllers
{
	public class BlogController : ControllerBase
	{
		private readonly IBlogQuery _blogQuery;
		private readonly IBlogUiQuery _blogUiQuery;
		public BlogController(IBlogQuery blogQuery, IBlogUiQuery blogUiQuery)
		{
			_blogQuery = blogQuery;
			_blogUiQuery = blogUiQuery;
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

		
		public async Task<IActionResult> Blogs(string slug = "", int pageId = 1, string q = "")
		{

			return View(  await _blogUiQuery.GetBlogsForUI(new(pageId, 6, q), slug));

		}
	}
}
