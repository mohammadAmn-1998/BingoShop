using Comments.Application.Contract.CommentService.Command;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Query.Contract.Ui.Comment;
using Shared.Application.Services;
using Shared.Domain.Enums;

namespace BingoShop.WebApplication.Controllers
{
	public class CommentController : ControllerBase
	{
		private readonly ICommentUIQuey _commentUiQuery;
		private readonly ICommentService _commentApplication;
		private readonly IAuthService _authService;

		public CommentController(ICommentUIQuey commentUiQuery, ICommentService commentApplication,
			IAuthService authService)
		{
			_commentUiQuery = commentUiQuery;
			_authService = authService;
			_commentApplication = commentApplication;
		}

		[Route("/Comments/{ownerId}/{commentFor}")]
		public async Task<IActionResult> Comments(long ownerId, CommentFor commentFor, int pageId = 1)
		{

			var model = await _commentUiQuery.GetCommentsForUI(ownerId, commentFor, pageId);
			var json = JsonConvert.SerializeObject(model);
			return Json(json);
		}

		[HttpPost]
		public async Task<IActionResult> Create(long ownerId, CommentFor commentFor, long? parentId,
			string? email, string fullName, string text)
		{
			var userId = _authService.GetUserId();
			CreateComment createComment = new CreateComment()
			{
				Email = email,
				For = commentFor,
				FullName = fullName,
				OwnerId = ownerId,
				ParentId = parentId,
				Text = text,
				UserId = userId
			};
			var res = await _commentApplication.Create(createComment);
			if (res.Status == Status.Success)
				SuccessAlert("نظر شما با موفقیت ثبت گردید!");
			else
				ErrorAlert(res.Message);
			return Redirect(Request.Headers["Referer"].ToString());
		}
	}
}
