using Shared.Application.Models;
using Shared.Application.Utility;

namespace Blogs1.Application.Contract.BlogCategoryService.Query;

public class FilteredBlogCategoryQueryModel : BasePagination
{

	public FilterParams FilterParams { get; set; }

	public List<BlogCategoryQueryModel>? BlogCategories { get; set; }

}