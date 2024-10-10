using Blogs1.Application.Contract.BlogCategoryService.Query;
using Shared.Application.Models;

namespace Blogs1.Application.Contract.BlogService.Query;

public interface IBlogQuery
{
	
	Task<FilteredBlogQueryModel?> GetFilteredPosts(FilterParams  filterParams);

	Task<List<PopularBlogQueryModel>> GetPopularBlogsForUI();
	List<LastBlogQueryModel> GetLastBlogsForUI();
	List<SpecialBlogForUIQueryModel> GetSpecialBlogsForUI();

	LastBlogTitleQueryModel GetBlogLastTitles();

}

public class LastBlogTitleQueryModel

{

	public List<BlogCategoryQueryModel> Categories { get; set; }

	public List<LastBlogQueryModel> Blogs { get; set; }

}