﻿using BingoShop.WebApplication.Utility;
using Blogs1.Application.Contract.BlogCategoryService.Query;
using Blogs1.Application.Contract.BlogService.Command;
using Blogs1.Application.Contract.BlogService.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Application.Models;
using Shared.Application.Services;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Blog
{
    [Area("Admin")]
    [RequiredPermission(UserPermission.پنل_مقالات)]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogQuery _blogQuery;
        private IBlogCategoryQuery _blogCategoryQuery;
        private readonly IBlogService _blogService;
        private readonly IAuthService _authService;
        private readonly IFileService _fileService;

        public BlogsController(IBlogCategoryQuery blogCategoryQuery, IBlogService blogService, IAuthService authService, IBlogQuery blogQuery, IFileService fileService)
        {
            _blogCategoryQuery = blogCategoryQuery;
            _blogService = blogService;
            _authService = authService;
            _blogQuery = blogQuery;
            _fileService = fileService;
        }
		
        public async Task<IActionResult> Index(string q = "", int pageId = 1 , int take=10)
        {

            var model = await _blogQuery.GetFilteredPosts(new FilterParams
            {
                PageId = pageId,
                Take = take,
                Title = q.Trim()
            });
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _blogCategoryQuery.GetAllCategoriesAsSelectList();

            var authorName = _authService.GetUserFullName();
            var userId = _authService.GetUserId();

            return View(new CreateBlog()
            {
                Categories = categories,
                UserId = userId,
                Author = authorName,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlog model)
        {

            if (model.CategoryId < 1)
            {
                ModelState.AddModelError(nameof(model.CategoryId), ErrorMessages.FieldIsRequired);
				model.Categories = await _blogCategoryQuery.GetAllCategoriesAsSelectList();
				return View(model);
            }

            List<SelectListItem>? categories;

            if (!ModelState.IsValid)
            {
                categories = await _blogCategoryQuery.GetAllCategoriesAsSelectList();
                if (model.SubCategoryId > 0)
                {
                    var subCategories = await _blogCategoryQuery.GetSubCategoriesAsSelectList(model.CategoryId);

                    model.SubCategories = subCategories;

                }

                model.Categories = categories;
                return View(model);
            }

            var result = await _blogService.Create(model);
            if (result.Status == Status.Success)
                return RedirectAndShowAlert(RedirectToAction("Index"), result);


            ErrorAlert(result.Message);
            categories = await _blogCategoryQuery.GetAllCategoriesAsSelectList();
            if (model.SubCategoryId > 0)
            {
                var subCategories = await _blogCategoryQuery.GetSubCategoriesAsSelectList(model.CategoryId);
                model.SubCategories = subCategories;
            }
            model.Categories = categories;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {

            if (id < 1)
                return RedirectAndShowAlert(RedirectToAction("Index"), OperationResult.NotFound());

            var model = await _blogService.GetForEdit(id);

            if (model == null)
                return RedirectAndShowAlert(RedirectToAction("Index"),
                    OperationResult.Error(ErrorMessages.BadRequestError));

            model.Categories = await _blogCategoryQuery.GetAllCategoriesAsSelectList();
            if (model.SubCategoryId > 0)
            {
                model.SubCategories = await _blogCategoryQuery.GetSubCategoriesAsSelectList(model.CategoryId);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlog model)
        {
            if (model.CategoryId < 1)
            {
                ModelState.AddModelError(nameof(model.CategoryId), ErrorMessages.FieldIsRequired);
               
			}

            if (!ModelState.IsValid)
            {
                model.Categories = await _blogCategoryQuery.GetAllCategoriesAsSelectList();

                if (model.SubCategoryId > 0)
                    model.SubCategories = await _blogCategoryQuery.GetSubCategoriesAsSelectList(model.CategoryId);
                return View(model);
            }

            var result = await _blogService.Edit(model);

            if (result.Status == Status.Success)
                return RedirectAndShowAlert(RedirectToAction("Index"), result);

            ErrorAlert(result.Message);
            model.Categories = await _blogCategoryQuery.GetAllCategoriesAsSelectList();
            if (model.SubCategoryId > 0)
	            model.SubCategories = await _blogCategoryQuery.GetSubCategoriesAsSelectList(model.CategoryId);
			return View(model);

        }

        public async Task<IActionResult> ChangeActivation(long blogId, int pageId = 1)
        {

            if (await _blogService.ActivationChange(blogId))
            {
                SuccessAlert();
            }
            else
            {
                ErrorAlert(ErrorMessages.InternalServerError);
            }

            return RedirectToAction("Index", new { pageId });

        }

        public async Task<IActionResult> ChangeSpecialation(long blogId, int pageId = 1)
        {
            if (await _blogService.SpecialationChange(blogId))
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

        [Route("Admin/Blogs/GetSubcategoriesSelectListItem/{parentId}")]
        public async Task<JsonResult> GetSubcategoriesSelectListItem(long parentId)
        {

            var subCategories = await _blogCategoryQuery.GetSubCategoriesAsSelectList(parentId);

            return Json(subCategories);

        }



        #endregion

    }
}
