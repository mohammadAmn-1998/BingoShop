using BingoShop.WebApplication.Utility;
using Blogs1.Application.Contract.BlogCategoryService.Command;
using Blogs1.Application.Contract.BlogCategoryService.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;
using IBlogCategoryService = Blogs1.Application.Contract.BlogCategoryService.Command.IBlogCategoryService;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Blog
{
    [Area("Admin")]
    [RequiredPermission(UserPermission.پنل_ادمین)]
    public class CategoriesController : ControllerBase
    {

        #region Ctor

        IBlogCategoryQuery _blogCategoryQuery;
        private readonly IBlogCategoryService _blogCategoryService;

        public CategoriesController(IBlogCategoryQuery blogCategoryQuery, IBlogCategoryService blogCategoryService)
        {
            _blogCategoryQuery = blogCategoryQuery;
            _blogCategoryService = blogCategoryService;
        }

        #endregion

        public async Task<IActionResult> Index(int pageId = 1, string q = "", int take = 10)
        {
            var model = await _blogCategoryQuery.GetFilteredBlogCategories(new FilterParams
            {
                PageId = pageId,
                Take = take,
                Title = q
            });

            return View(model);
        }

        public async Task<IActionResult> Create(long parentId = 0)
        {
            if (parentId > 0)
            {
                var categories = await _blogCategoryQuery.GetAllCategoriesAsSelectList();
                var subCategories = await _blogCategoryQuery.GetSubCategoriesAsSelectList(parentId);

                ViewData["ParentId"] = parentId;
                ViewData["ParentTitle"] = await _blogCategoryQuery.GetBlogCategoryTitle(parentId);

            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogCategory model)
        {

            if (!ModelState.IsValid)
            {
	            ViewData["ParentId"] = model.ParentId;
	            ViewData["ParentTitle"] = await _blogCategoryQuery.GetBlogCategoryTitle(model.ParentId);
				return View(model);
            }

            var result = await _blogCategoryService.CreateCategory(model);

            if (result.Status == Status.Success)
            {
                SuccessAlert("دسته بندی ایجاد شد!");
                return View(model);
            }

            ErrorAlert(result.Message);
            ViewData["ParentId"] = model.ParentId;
            ViewData["ParentTitle"] = await _blogCategoryQuery.GetBlogCategoryTitle(model.ParentId);
			return View(model);
        }

        [HttpGet]
        [Route("/Admin/Categories/Edit/{id}")]
        public async Task<IActionResult> Edit(long id)
        {
            var model = await _blogCategoryService.GetCategoryForEdit(id);
            if (model == null) return RedirectAndShowAlert(Redirect("/Admin/Categories/Index"),
                    OperationResult.Error(ErrorMessages.InternalServerError));

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogCategory model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _blogCategoryService.EditCategory(model);

            if (result.Status == Status.Success)
            {
                return RedirectAndShowAlert(RedirectToAction("Index"), result);
            }

            ErrorAlert(result.Message);
            return View(model);

        }

        public async Task<IActionResult> ChangeActivation(long categoryId, int pageId = 1)
        {
            if (await _blogCategoryService.ChangeActivation(categoryId))
            {
                SuccessAlert();
            }
            else
            {
                ErrorAlert(ErrorMessages.InternalServerError);
            }

            return RedirectToAction("Index", new { pageId });
        }

        #region Handlers

        public async Task<IActionResult> GoToCreateModal()
        {
            return PartialView("Modals/_CreateCategoryModal");
        }

        #endregion
    }
}
