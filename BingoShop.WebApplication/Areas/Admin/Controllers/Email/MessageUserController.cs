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
	[RequiredPermission(UserPermission.AdminPanel)]
	public class MessageUserController : ControllerBase
	{

		private readonly IMessageUserAdminQuery _MessageUserAdminQuery;
		private readonly IMessageUserApplication _MessageUserApplication;

		public MessageUserController(IMessageUserAdminQuery MessageUserAdminQuery, IMessageUserApplication MessageUserApplication)
		{
			_MessageUserAdminQuery = MessageUserAdminQuery;
			_MessageUserApplication = MessageUserApplication;
		}

		public async Task<IActionResult> Index(int pageId =1,string q = "",int take=10,MessageStatus? status = null )
		{
			return View(await _MessageUserAdminQuery.GetMessageUsersForAdmin( new FilterParams(pageId,take,q),status));
		}

		public IActionResult MessageDetail(long id)
		{
			return View( _MessageUserAdminQuery.GetMessageUserDetailForAdmin(id));

		}


		public IActionResult ChangeStatus(long id, string text, MessageStatus status)
		{
			OperationResult result;
			switch (status)
			{
				case MessageStatus.پاسخ_داده_شد_sms:

					//
					//
					//send sms...
					// 
					//
					//
					 result = _MessageUserApplication.AnsweredBySMS(id,text);
					if(result.Status == Status.Success)
						SuccessAlert("جواب  با موفقیت فرستاده شد!");
					else
						ErrorAlert(result.Message);
					break;

				case MessageStatus.پاسخ_داده_شد_email:

					//
					//
					//send email...
					// 
					//
					//

					result = _MessageUserApplication.AnsweredByEmail(id, text);
					if (result.Status == Status.Success)
						SuccessAlert("جواب با موفقیت فرستاده شد!");
					else
						ErrorAlert(result.Message);
					
					break;

				 default:
					 if( _MessageUserApplication.AnswerByCall(id))
						  SuccessAlert("وضعیت جواب : تماس گرفته شده");
					 else
						  ErrorAlert(ErrorMessages.InternalServerError);
					  
					 break;
			}

			return RedirectToAction("MessageDetail", new { id = id });



		}
	}
}
