using BingoShop.WebApplication.Utility;
using Emails.Application.Contract.MessageUserApplication.Command;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.MessageUser;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Email
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.پنل_خبرنامه)]
	public class MessageUsersController : ControllerBase
	{

		private readonly IMessageUserAdminQuery _MessageUserAdminQuery;
		private readonly IMessageUserApplication _MessageUserApplication;

		public MessageUsersController(IMessageUserAdminQuery MessageUserAdminQuery, IMessageUserApplication MessageUserApplication)
		{
			_MessageUserAdminQuery = MessageUserAdminQuery;
			_MessageUserApplication = MessageUserApplication;
		}

		public async Task<IActionResult> Index(int pageId =1,string q = "",int take=10,MessageStatus? status = null )
		{
			return View(await _MessageUserAdminQuery.GetMessageUsersForAdmin( new FilterParams(pageId,take,q),status));
		}

		public async Task<IActionResult> MessageDetail(long id)
		{
			return View(await _MessageUserAdminQuery.GetMessageUserDetailForAdmin(id));

		}

		[HttpPost]
		public IActionResult ChangeStatus(  AnswerMessageUser model)
		{
			OperationResult result;
			switch (model.Status)
			{
				case MessageStatus.پاسخ_داده_شد_sms:
					if (string.IsNullOrEmpty(model.AnswerSms?.Trim()))
					{
						ErrorAlert("متن جواب خالی است!");
						break;
					}
					//
					//
					//send sms...
					// 
					//
					//

					result = _MessageUserApplication.AnsweredBySMS(model.Id, model.AnswerSms);
					if(result.Status == Status.Success)
						SuccessAlert("جواب  با موفقیت فرستاده شد!");
					else
						ErrorAlert(result.Message);
					break;

				case MessageStatus.پاسخ_داده_شد_email:

					if (string.IsNullOrEmpty(model.AnswerEmail?.Trim()))
					{
						ErrorAlert("متن جواب خالی است!");
						break;
					}

					//
					//
					//send email...
					// 
					//
					//

					result = _MessageUserApplication.AnsweredByEmail(model.Id, model.AnswerEmail);
					if (result.Status == Status.Success)
						SuccessAlert("جواب با موفقیت فرستاده شد!");
					else
						ErrorAlert(result.Message);
					
					break;

				 default:
					 if( _MessageUserApplication.AnswerByCall(model.Id))
						  SuccessAlert("وضعیت جواب : تماس گرفته شده");
					 else
						  ErrorAlert(ErrorMessages.InternalServerError);
					  
					 break;
			}

			return RedirectToAction("MessageDetail", new { id = model.Id });



		}
	}
}
