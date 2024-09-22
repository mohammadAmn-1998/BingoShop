using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Application.Utility;

namespace BingoShop.WebApplication.Controllers
{
	public class ControllerBase : Controller
	{


		#region SuccessAlert

		public void SuccessAlert(bool isReload = false)
		{

			var model = JsonConvert.SerializeObject(OperationResult.Success());
			HttpContext.Response.Cookies.Append("SystemAlert", model);

		}

		public void SuccessAlert(string message, bool isReload = false)
		{

			var model = JsonConvert.SerializeObject(OperationResult.Success(message));
			HttpContext.Response.Cookies.Append("SystemAlert", model);

		}

		#endregion

		#region ErrorAlert

		public void ErrorAlert(bool isReload = false)
		{
			var model = JsonConvert.SerializeObject(OperationResult.Error());
			HttpContext.Response.Cookies.Append("SystemAlert", model);


		}
		public void ErrorAlert(string message, bool isReload = false)
		{
			var model = JsonConvert.SerializeObject(OperationResult.Error(message));
			HttpContext.Response.Cookies.Append("SystemAlert", model);


		}

		#endregion

		#region NotFoundAlert

		public void NotFoundAlert(bool isReload = false)
		{
			var model = JsonConvert.SerializeObject(OperationResult.NotFound());
			HttpContext.Response.Cookies.Append("SystemAlert", model);


		}
		public void NotFoundAlert(string message, bool isReload = false)
		{
			var model = JsonConvert.SerializeObject(OperationResult.NotFound(message));
			HttpContext.Response.Cookies.Append("SystemAlert", model);


		}

		#endregion


		public IActionResult RedirectAndShowAlert(IActionResult redirectResult, OperationResult operationResult)
		{

			var model = JsonConvert.SerializeObject(operationResult);
			HttpContext.Response.Cookies.Append("SystemAlert", model);

			return redirectResult;
		}
	}
}
