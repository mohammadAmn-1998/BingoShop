using Blogs1.Application.Contract.BlogCategoryService.Query;
using Microsoft.AspNetCore.Mvc;

namespace BingoShop.WebApplication.ViewComponents
{
	public class BlogWidgetCategoriesViewComponent : ViewComponent
	{

		private readonly IBlogCategoryQuery _blogCategoryQuery;

		public BlogWidgetCategoriesViewComponent(IBlogCategoryQuery blogCategoryQuery)
		{
			_blogCategoryQuery = blogCategoryQuery;
		}


		public async Task<IViewComponentResult> InvokeAsync()
		{

			return View(await _blogCategoryQuery.GetCategoriesForWidgetForUi(6));

		}

	}
}
