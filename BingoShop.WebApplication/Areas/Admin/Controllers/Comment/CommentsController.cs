using BingoShop.WebApplication.Utility;
using Comments.Application.Contract.CommentService.Command;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.Comment;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;


namespace BingoShop.WebApplication.Areas.Admin.Controllers.Comment
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.AdminPanel)]
	public class CommentsController : ControllerBase
	{

		private readonly ICommentAdminQuery _CommentAdminQuery;
		private readonly ICommentService _CommentApplication;

		public CommentsController(ICommentAdminQuery CommentAdminQuery, ICommentService CommentApplication)
		{
			_CommentAdminQuery = CommentAdminQuery;
			_CommentApplication = CommentApplication;
		}
		
		public async Task<IActionResult> Index( CommentFor commentFor, CommentStatus commentStatus , int take = 10, int pageId = 1, string q = ""
			)
		{
			return View(await _CommentAdminQuery.GetCommentsForAdmin(new FilterParams(pageId, take, q), commentStatus,
				commentFor));
		}

		public async Task<IActionResult> Detail(long id)
		{
			return View( await _CommentAdminQuery.GetCommentDetailForAdmin(id));
		}

		public IActionResult ChangeStatus(long id)
		{

			return View(new ChangeCommentStatus() { Id = id });
		}

		[HttpPost]
		public async Task<IActionResult> ChangeStatus(ChangeCommentStatus model)
		{

			if (!ModelState.IsValid)
				return View(model);

			if (model.CommentStatus == CommentStatus.هنوز_دیده_نشده)
			{
				ErrorAlert("لطفا اول برای این کامنت تعیین وضعیت کنید وسپس دکمه ثبت را بزنید!");
				return View(model);
			}

			if (model.CommentStatus == CommentStatus.رد_شده)
			{
				if (string.IsNullOrEmpty(model.WhyRejected))
				{
					ErrorAlert("لطفا دلیل رد شدن کامنت را بنویسید!");
					return View(model);
				}

				if (!await _CommentApplication.RejectCommentByAdmin(model.Id, model.WhyRejected))
					ErrorAlert(ErrorMessages.InternalServerError);

			}

			if (model.CommentStatus == CommentStatus.قبول_شده)
			{
				if (!await _CommentApplication.ApproveCommentByAdmin(model.Id))
					ErrorAlert(ErrorMessages.InternalServerError);
			}

			return RedirectAndShowAlert(RedirectToAction("Index"),
				OperationResult.Success($"کامنت مورد نظر تعیین وضعیت شد!"));


		}
	

}
}
