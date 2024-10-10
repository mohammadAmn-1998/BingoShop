namespace Blogs1.Application.Contract.BlogService.Query
{
	public class LastBlogQueryModel
	{

		public long Id { get; set; }

		public string Title { get; set; }

		public string Summary { get; set; }

		public string Slug { get; set; }

		public string ImageName { get; set; }

		public string ImageAlt { get; set; }

		public string CategoryTitle { get; set; }

		public string CreateDate { get; set; }

		public string Author { get; set; }
	}
}