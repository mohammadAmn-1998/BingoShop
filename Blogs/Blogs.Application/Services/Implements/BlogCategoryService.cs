using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Blogs.Application.Dtos.BlogCategories;
using Blogs.Application.Services.Interfaces;
using Blogs.Domain.Agg.CategoryAgg;
using Shared.Application.Services;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Blogs.Application.Services.Implements
{
	internal class BlogCategoryService : IBlogCategoryService
	{

		private readonly IBlogCategoryRepository _blogCategoryRepository;
		private readonly IFileService _fileService;
		


		public BlogCategoryService(IBlogCategoryRepository blogCategoryRepository, IFileService fileService)
		{
			_blogCategoryRepository = blogCategoryRepository;
			_fileService = fileService;
		}

		public OperationResult Create(CreateBlogCategoryDto dto)
		{


			if (_blogCategoryRepository.IsExists(x => x.Title == dto.Title.Trim()))
				return new(Status.BadRequest, ErrorMessages.DuplicateError, dto.Title);

			var slug = dto.Slug.GenerateSlug();

			if (_blogCategoryRepository.IsExists(x => x.Slug == slug))
				return new(Status.BadRequest, ErrorMessages.DuplicateError, dto.Slug);

			if (dto.ImageFile == null || !dto.ImageFile.IsImage())
				return new(Status.BadRequest, "لطفا یک عکس معتبر انخاب کنید!", dto.ImageFile?.Name);

			var imageName =
				_fileService.UploadFileAndReturnFileName(dto.ImageFile, Directories.BlogCategoryImageDirectory);

			if (imageName is null)
				return new(Status.InternalServerError, "مشکلی هنگام آپلود عکس بوجود آمده است! دوباره تلاش کنید",
					dto.ImageFile.Name);

			_fileService.ResizeImage(imageName, Directories.BlogCategoryImageDirectory400,400);
			_fileService.ResizeImage(imageName, Directories.BlogCategoryImageDirectory100,100);

			if (_blogCategoryRepository.Insert(new BlogCategory(dto.Title, imageName, dto.ParentId, dto.Slug,
				    dto.ImageAlt)))
				return new(Status.Success, "فهرست جدید ایجاد شد!");

			//if insert was not successful we need to delete uploaded images :
			_fileService.DeleteFile(imageName, Directories.BlogCategoryImageDirectory);
			_fileService.DeleteFile(imageName, Directories.BlogCategoryImageDirectory400);
			_fileService.DeleteFile(imageName, Directories.BlogCategoryImageDirectory100);

			return new(Status.InternalServerError, "مشکلی در سرور وجود دارد!دوباره تلاش کنید.");


		}

		public OperationResult Edit(EditBlogCategoryDto dto)
		{

			try
			{
				var blogCategory = _blogCategoryRepository.GetBy(x => x.Id == dto.Id);
				if (blogCategory is null)
					throw new NullReferenceException();

				var editedSlug = dto.Slug.GenerateSlug();

				if (blogCategory.Slug != dto.Slug)
				{
					
					if (_blogCategoryRepository.IsExists(x => x.Slug == editedSlug))
						return new(Status.BadRequest, "اسلاگ تکراری است!", "Slug");
				}

				if (blogCategory.Title != dto.Title.Trim())
					{
						if (_blogCategoryRepository.IsExists(x => x.Title == dto.Title.Trim()))
							return new(Status.BadRequest, "عنوان تکراری است!", "Title");
					}

					if (dto.ImageFile != null)
					{
						var newImageName = _fileService.UploadFileAndReturnFileName(dto.ImageFile,
							Directories.BlogCategoryImageDirectory);

						if (newImageName is null)
							return new(Status.InternalServerError, "مشکلی هنگام آپلود عکس بوجود آمده است! دوباره تلاش کنید",
								dto.ImageFile.Name);

						_fileService.ResizeImage(newImageName, Directories.BlogCategoryImageDirectory400, 400);
						_fileService.ResizeImage(newImageName, Directories.BlogCategoryImageDirectory100, 100);

						blogCategory.Edit(dto.Title,newImageName,editedSlug,dto.ImageAlt);
						if (_blogCategoryRepository.Update(blogCategory))
							return new(Status.Success, "فهرست ویرایش شد!");

						//if Update was not successful we need to delete uploaded images :
						_fileService.DeleteFile(newImageName, Directories.BlogCategoryImageDirectory);
						_fileService.DeleteFile(newImageName, Directories.BlogCategoryImageDirectory400);
						_fileService.DeleteFile(newImageName, Directories.BlogCategoryImageDirectory100);

						return new(Status.InternalServerError, "مشکلی در سرور به وجود آمده! لطفا دوباره تلاش کنید.");


					}

					blogCategory.Edit(dto.Title, dto.ImageName, editedSlug, dto.ImageAlt);
					if (_blogCategoryRepository.Update(blogCategory))
						return new(Status.Success, "فهرست ویرایش شد!");

					return new(Status.InternalServerError, "مشکلی در سرور به وجود آمده! لطفا دوباره تلاش کنید.");
			}
			catch (Exception e)
			{
				return new(Status.InternalServerError, "مشکلی در سرور به وجود آمده! لطفا دوباره تلاش کنید.");
			}
		
		}

		public EditBlogCategoryDto GetForEdit(int categoryId)
		{
			var blogCategory = _blogCategoryRepository.GetBy(x => x.Id == categoryId);

			if (blogCategory == null)
				return new();

			return new EditBlogCategoryDto
			{
				Id = blogCategory.Id,
				Title = blogCategory.Title,
				ImageName = blogCategory.ImageName,
				ImageAlt = blogCategory.ImageAlt,
				Slug = blogCategory.Slug
			};
		}

		public bool Exists(Expression<Func<BlogCategory, bool>> expression)
		{
			return _blogCategoryRepository.IsExists(expression);
		}

		public void ActivationChange(int categoryId)
		{
			var blogCategory = _blogCategoryRepository.GetBy(x => x.Id == categoryId);

			blogCategory?.ActivationChange();
		}
	}
}
