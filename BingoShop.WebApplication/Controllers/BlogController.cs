
using Blogs1.Application.Contract.BlogService.Command;
using Blogs1.Application.Contract.BlogService.Query;
using Comments.Application.Contract.CommentService.Command;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.Ui.Blog;
using Shared.Application.Models;

namespace BingoShop.WebApplication.Controllers
{
	public class BlogController : ControllerBase
	{
		private readonly IBlogQuery _blogQuery;
		private readonly IBlogUiQuery _blogUiQuery;
		private readonly IBlogService _blogService;
		public BlogController(IBlogQuery blogQuery, IBlogUiQuery blogUiQuery, IBlogService blogService)
		{
			_blogQuery = blogQuery;
			_blogUiQuery = blogUiQuery;
			_blogService = blogService;
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

		[Route("/Blog/{slug}")]
		public async Task<IActionResult> Blog(string slug)
		{
			await _blogService.IncreaseVisits(slug);
			return View(await _blogUiQuery.GetSingleBlog(slug));

		}
	}
}
