using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Application.Models;

namespace Blogs1.Application.Contract.BlogCategoryService.Query;

public interface IBlogCategoryQuery
{

	Task<FilteredBlogCategoryQueryModel?> GetFilteredBlogCategories(FilterParams filterParams);

	Task<List<BlogCategoryQueryModel>?> GetAllCategories();
	Task<List<BlogCategoryQueryModel>?> GetSubCategories(long parentId);


	Task<List<SelectListItem>?> GetAllCategoriesAsSelectList();
	Task<List<SelectListItem>?> GetSubCategoriesAsSelectList(long parentId);

	Task<string> GetBlogCategoryTitle(long id);

	Task<List<WidgetCategoryForUIQueryModel>> GetCategoriesForWidgetForUi(int count);



}

public class WidgetCategoryForUIQueryModel
{

	public string Title { get; set; }

	public string Slug { get; set; }

	public long BlogsCount { get; set; }

}