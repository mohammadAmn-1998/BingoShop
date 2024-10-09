using Blogs1.Application.Contract.BlogCategoryService.Query;
using Microsoft.AspNetCore.Mvc;

namespace BingoShop.WebApplication.ViewComponents
{
	public class BlogTopNavMenuViewComponent : ViewComponent
	{

		private readonly IBlogCategoryQuery _blogCategoryQuery;

		public BlogTopNavMenuViewComponent(IBlogCategoryQuery blogCategoryQuery)
		{
			_blogCategoryQuery = blogCategoryQuery;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{

			var model = await _blogCategoryQuery.GetAllCategories();
			return View(model);

		}

	}
}
