using BingoShop.WebApplication.Utility;
using Microsoft.AspNetCore.Mvc;
using PostModule.Application.Contract.PostPriceApplication;
using PostModule.Application.Contract.PostQuery;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Post
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.AdminPanel)]
	public class PostPricesController : ControllerBase
	{
		private readonly IPostQuery _postQuery;
		private readonly IPostPriceApplication _postPriceApplication;

		public PostPricesController(IPostPriceApplication postPriceApplication, IPostQuery postQuery)
		{
			_postPriceApplication = postPriceApplication;
			_postQuery = postQuery;
		}

		public IActionResult Index(int id) => View(_postQuery.GetPostDetails(id));

		public IActionResult Create(int id) => View(new CreatePostPrice { PostId = id });
		[HttpPost]
		public IActionResult Create(int id, CreatePostPrice model)
		{
			if (id != model.PostId) return NotFound();
			if (!ModelState.IsValid) return View(model);
			var res = _postPriceApplication.Create(model);
			if (res.Status == Status.Success)
			{
				SuccessAlert();
				return Redirect($"/Admin/PostPrice/Index/{id}");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}

		public IActionResult Edit(int id) => View(_postPriceApplication.GetForEdit(id));
		[HttpPost]
		public IActionResult Edit(int id, EditPostPrice model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _postPriceApplication.Edit(model);
			if (res.Status == Status.Success)
			{
				SuccessAlert();
				return Redirect($"/Admin/PostPrice/Edit/{id}");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}




	}
}
