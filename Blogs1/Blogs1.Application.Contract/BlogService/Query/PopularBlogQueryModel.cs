using Blogs1.Application.Contract.BlogCategoryService.Query;

namespace Blogs1.Application.Contract.BlogService.Query;

	public class PopularBlogQueryModel
	{

		public long Id { get; set; }

		public string Title { get; set; }

		public string Author { get; set; }

		public string CreateDate { get; set; }

		public string ImageName { get; set; }

		public string ImageAlt { get; set; }

		public long  Likes { get; set; }

		public string Slug { get; set; }


	}

public class SpecialBlogForUIQueryModel : PopularBlogQueryModel
{
	public BlogCategoryQueryModel Category { get; set; }
	public BlogCategoryQueryModel? SubCategory { get; set; }


}