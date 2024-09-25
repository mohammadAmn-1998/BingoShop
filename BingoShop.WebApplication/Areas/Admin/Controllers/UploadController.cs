using Microsoft.AspNetCore.Mvc;
using Shared.Application.Services;
using Shared.Application.Utility;

namespace BingoShop.WebApplication.Areas.Admin.Controllers
{
	public class UploadController : Controller
	{
		
		private readonly IFileService _fileService;

		public UploadController(IFileService fileService)
		{
			_fileService = fileService;
		}

		public JsonResult Article(IFormFile? upload)
		{

			if (upload == null)
			{
				BadRequest();
				return Json(new { Uploaded = false });
			}


			var imageName = _fileService.UploadFileAndReturnFileName(upload!, Directories.BlogContentImageDirectory);

			if (imageName != null)
			{
				return Json(new { Uploaded = true, Url = Directories.GetBlogContentImageFullPath(imageName) });
			}

			return Json(new { Uploaded = false });
		}
	}
}
