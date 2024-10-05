using BingoShop.WebApplication.Utility;
using Blogs1.Application.Contract.BlogCategoryService.Command;
using Blogs1.Application.Contract.BlogCategoryService.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Site.Application.Contract.ImageSiteApplication.Command;
using Site.Application.Contract.ImageSiteApplication.Query;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Site
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.AdminPanel)]
	public class ImageSiteController : ControllerBase
	{
		private readonly IImageSiteQuery _ImageSiteQuery;
		private readonly IImageSiteApplication _ImageSiteApplication;

		public ImageSiteController(IImageSiteQuery ImageSiteQuery, IImageSiteApplication ImageSiteApplication)
		{
			_ImageSiteQuery = ImageSiteQuery;
			_ImageSiteApplication = ImageSiteApplication;
		}

		public async Task<IActionResult> Index(int pageId = 1, string q = "", int take = 2)
		{
			var model =  _ImageSiteQuery.GetAllForAdmin(pageId,take,q);

			return View(model);
		}

		public async Task<IActionResult> Create()
		{
			
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateImageSite model)
		{

			if (!ModelState.IsValid)  
				return View(model);
			

			var result = await _ImageSiteApplication.Create(model);

			if (result.Status == Status.Success)
			{
				SuccessAlert("تصویر سایت ایجاد شد!");
				return View(model);
			}

			ErrorAlert(result.Message);
			return View(model);
		}

		public async Task<bool> DeleteImage(int id) => await _ImageSiteApplication.DeleteFromDataBase(id);


	}
}
