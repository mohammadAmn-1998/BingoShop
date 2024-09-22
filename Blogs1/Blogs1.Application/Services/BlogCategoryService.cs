using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs1.Application.Contract.BlogCategoryService.Command;
using Blogs1.Domain.BlogAgg.IRepositories;
using Shared.Application.Services;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Blogs1.Application.Services
{
	public class BlogCategoryService : IBlogCategoryService
	{

		#region ctor

		private readonly IBlogCategoryRepository _blogCategoryRepository;
		private readonly IFileService _fileService;
		public BlogCategoryService(IBlogCategoryRepository blogCategoryRepository, IFileService fileService)
		{
			_blogCategoryRepository = blogCategoryRepository;
			_fileService = fileService;
		}

		#endregion


		public async Task<OperationResult> CreateCategory(CreateBlogCategory command)
		{
			//check Title
			if (await _blogCategoryRepository.Exists(x => x.Title == command.Title.Trim()))
				return new(Status.BadRequest, ErrorMessages.DuplicateTitleError, "Title");

			//check slug
			command.Slug = command.Slug.GenerateSlug();
			if (await _blogCategoryRepository.Exists(x => x.Slug == command.Slug.Trim()))
				return new(Status.BadRequest, ErrorMessages.DuplicateSlugError, "Slug");

			if (command.ParentId > 0)
			{
				if (!await _blogCategoryRepository.Exists(x => x.Id == command.ParentId))
					return new(Status.BadRequest, ErrorMessages.BlogCategoryNotFound, "");
			}

			string? imageName;
			if (command.ImageFile is not null)
			{
				if (!command.ImageFile.IsImage())
					return new(Status.BadRequest, ErrorMessages.IsNotImage, "ImageFile");

				imageName = _fileService.UploadFileAndReturnFileName(command.ImageFile,Directories.BlogCategoryImageDirectory);
				if (imageName == null)
					return new(Status.InternalServerError, ErrorMessages.UploadIsInvalid);
				command.ImageName = imageName;
				_fileService.ResizeImage(imageName, Directories.BlogCategoryImageDirectory, 100);
				_fileService.ResizeImage(imageName, Directories.BlogCategoryImageDirectory, 400);

				var ok = await _blogCategoryRepository.Create(command);
				if (!ok)
				{
					_fileService.DeleteFile(imageName, Directories.BlogCategoryImageDirectory);
					_fileService.DeleteFile(imageName, Directories.BlogCategoryImageDirectory100);
					_fileService.DeleteFile(imageName, Directories.BlogCategoryImageDirectory400);

					return new(Status.InternalServerError);
				}

				return new(Status.Success);

			}

			command.ImageName = "Default.png";

			return  await _blogCategoryRepository.Create(command)
				? OperationResult.Success()
				: new OperationResult(Status.InternalServerError);
		}

		public async Task<OperationResult> EditCategory(EditBlogCategory command)
		{
			var blogCategory = await _blogCategoryRepository.GetBy(command.Id);

			if (blogCategory.Title.Trim() != command.Title.Trim())
			{
				if (await _blogCategoryRepository.Exists(x => x.Title == command.Title.Trim()))
					return new(Status.BadRequest, ErrorMessages.DuplicateTitleError,"Title");
			}

			command.Slug = command.Slug.GenerateSlug();

			if (blogCategory.Slug.Trim() != command.Slug.Trim())
			{
				if (await _blogCategoryRepository.Exists(x => x.Slug == command.Slug.Trim()))
					return new(Status.BadRequest, ErrorMessages.DuplicateSlugError,"Slug");
			}

			string? imageName;
			if (command.ImageFile is not null)
			{
				if (!command.ImageFile.IsImage())
					return new(Status.BadRequest, ErrorMessages.IsNotImage, "ImageFile");

				imageName = _fileService.UploadFileAndReturnFileName(command.ImageFile, Directories.BlogCategoryImageDirectory);
				if (imageName == null)
					return new(Status.InternalServerError, ErrorMessages.UploadIsInvalid);
				command.ImageName = imageName;
				_fileService.ResizeImage(imageName, Directories.BlogCategoryImageDirectory, 100);
				_fileService.ResizeImage(imageName, Directories.BlogCategoryImageDirectory, 400);

				var ok = await _blogCategoryRepository.Update(command);
				if (!ok)
				{
					_fileService.DeleteFile(imageName, Directories.BlogCategoryImageDirectory);
					_fileService.DeleteFile(imageName, Directories.BlogCategoryImageDirectory100);
					_fileService.DeleteFile(imageName, Directories.BlogCategoryImageDirectory400);

					return new(Status.InternalServerError);
				}

				return new(Status.Success);

			}

			

			return await _blogCategoryRepository.Update(command)
				? OperationResult.Success()
				: new OperationResult(Status.InternalServerError);


		}

		public async Task<OperationResult> DeleteCategory(long id)
		{
			throw new NotImplementedException();
		}

		public async Task<EditBlogCategory?> GetCategoryForEdit(long id)
		{
			try
			{
				var blogCategory = await _blogCategoryRepository.GetBy(id);
				if (blogCategory == null)
					throw new NullReferenceException();

				return new()
				{
					Title = blogCategory.Title,
					ImageFile = null,
					ImageName = blogCategory.ImageName,
					ImageAlt = blogCategory.ImageAlt,
					ParentId = blogCategory.ParentId,
					Slug = blogCategory.Slug,
					Id = blogCategory.Id,
					CreateDate = blogCategory.CreateDate,
					IsActive = blogCategory.Active
				};
			}
			catch (Exception e)
			{
				return null;
			}
		}

		public async Task<bool> ChangeActivation(long categoryId)
		{
			return await _blogCategoryRepository.ChangeActivation(categoryId);
		}

		#region Private Methods

		private async Task<bool> Exists(int categoryId) => await _blogCategoryRepository.Exists(x => x.Id == categoryId);


		#endregion

	}
}
