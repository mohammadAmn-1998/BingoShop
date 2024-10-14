using Shared.Application.Models;
using Shared.Domain.Enums;

namespace Query.Contract.Ui.Blog;

public interface IBlogUiQuery
{
	Task<BlogUIPaging> GetBlogsForUI(FilterParams filterParams, string categorySlug = "");
	Task<SingleBlogUIQueryModel> GetSingleBlog(string slug);
	Task<SingleBlogUIQueryModel> GetSingleBlog(long  ownerId);
}