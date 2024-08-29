using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Shared.Application.Utility;

namespace Users1.WebUI.Controllers
{
	public class ControllerBase : Controller
	{


		#region SuccessAlert

		public async void SuccessAlert(bool isReload = false)
		{

			var model = JsonConvert.SerializeObject(OperationResult.Success());
			HttpContext.Response.Cookies.Append("SystemAlert", model);

		}

		public async void SuccessAlert(string message, bool isReload = false)
		{

			var model = JsonConvert.SerializeObject(OperationResult.Success(message));
			HttpContext.Response.Cookies.Append("SystemAlert", model);
			
		}

		#endregion

		#region ErrorAlert

		public async void ErrorAlert(bool isReload = false)
		{
			var model = JsonConvert.SerializeObject(OperationResult.Error());
			HttpContext.Response.Cookies.Append("SystemAlert", model);
			

		}
		public async void ErrorAlert(string message , bool isReload = false)
		{
			var model = JsonConvert.SerializeObject(OperationResult.Error(message));
			HttpContext.Response.Cookies.Append("SystemAlert", model);
			

		}

		#endregion

		#region NotFoundAlert

		public async void NotFoundAlert(bool isReload = false)
		{
			var model = JsonConvert.SerializeObject(OperationResult.NotFound());
			HttpContext.Response.Cookies.Append("SystemAlert", model);
		

		}
		public async void NotFoundAlert(string message, bool isReload = false)
		{
			var model = JsonConvert.SerializeObject(OperationResult.NotFound(message));
			HttpContext.Response.Cookies.Append("SystemAlert", model);


		}

		#endregion


		public IActionResult RedirectAndShowAlert(RedirectResult redirectResult, OperationResult operationResult)
		{

			var model = JsonConvert.SerializeObject(operationResult);
			HttpContext.Response.Cookies.Append("SystemAlert",model);

			return redirectResult;
		}
	}
}
