namespace Blogs1.Application.Contract.BlogCategoryService.Command
{
	public class EditBlogCategory : CreateBlogCategory
	{

		public long Id { get; set; }

		public DateTime CreateDate { get; set; }

		public bool IsActive { get; set; }
	}
}