using Microsoft.AspNetCore.Mvc;
using Query.Contract.Ui.Comment;
using Shared.Domain.Enums;

namespace BingoShop.WebApplication.ViewComponents
{
	public class BlogCommentsViewComponent : ViewComponent
	{

		private readonly ICommentUIQuey _commentUIQuery;

		public BlogCommentsViewComponent(ICommentUIQuey commentUiQuery)
		{
			_commentUIQuery = commentUiQuery;
		}


		public async Task<IViewComponentResult> InvokeAsync(long ownerId,int pageId=1)
		{
			var comments =await _commentUIQuery.GetCommentsForUI(1, CommentFor.بلاگ ,pageId);
			return View(comments);

		}
	}
}
