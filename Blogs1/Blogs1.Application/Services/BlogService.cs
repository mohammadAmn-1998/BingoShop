using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Blogs1.Application.Contract.BlogService.Command;
using Blogs1.Domain.BlogAgg.IRepositories;
using Shared.Application.Services;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Blogs1.Application.Services
{
	internal class BlogService : IBlogService
	{

		#region Ctor

		private readonly IBlogRepository _blogRepository;
		private readonly IFileService _fileService;

		public BlogService(IBlogRepository blogRepository, IFileService fileService)
		{
			_blogRepository = blogRepository;
			_fileService = fileService;
		}

		#endregion

		public async Task<OperationResult> Create(CreateBlog command)
		{
			//Check that the title is not duplicated
			if (await _blogRepository.Exists(x=> x.Title.Trim() == command.Title.Trim()))
			  return new(Status.BadRequest, ErrorMessages.DuplicateTitleError, "Title");

			//Then Check the slug is not duplicated after generated as slug
			command.Slug = command.Slug.Trim().GenerateSlug();
			if (await _blogRepository.Exists(x => x.Slug.Trim() == command.Slug.Trim()))
				return new(Status.BadRequest, ErrorMessages.DuplicateTitleError, "Slug");

			//check image choosed or not
			if (command.ImageFile is not null)
			{
				if (!command.ImageFile.IsImage())
					return new(Status.BadRequest, ErrorMessages.IsNotImage, "ImageFile");

				//store image in database and return imageName
				var imageName = _fileService.UploadFileAndReturnFileName(command.ImageFile,Directories.BlogImageDirectory);

				if (imageName is null)
					return new(Status.InternalServerError, ErrorMessages.InternalServerError);

				command.ImageName = imageName;

				_fileService.ResizeImage(imageName, Directories.BlogCategoryImageDirectory, 100);
				_fileService.ResizeImage(imageName, Directories.BlogCategoryImageDirectory, 400);

				var ok =await _blogRepository.Create(command);

				if (!ok)
				{

					//Delete Images That Uploaded
					_fileService.DeleteFile(imageName, Directories.BlogImageDirectory);
					_fileService.DeleteFile(imageName, Directories.BlogImageDirectory100);
					_fileService.DeleteFile(imageName, Directories.BlogImageDirectory400);

					return new(Status.InternalServerError,ErrorMessages.InternalServerError);
				}

				return new(Status.Success);
			}

			command.ImageName = "Default.jpg";

			if ( !await _blogRepository.Create(command))
				return new(Status.InternalServerError, ErrorMessages.InternalServerError);

			return new(Status.Success);
		}

		public async Task<OperationResult> Edit(EditBlog command)
		{
			//Check title if changed  not Duplicate
			var blog = await _blogRepository.GetForEdit(command.Id);
			if (blog?.Title.Trim() != command.Title.Trim())
			{
				if (await _blogRepository.Exists(x => x.Title == command.Title.Trim()))
					return new(Status.BadRequest, ErrorMessages.DuplicateTitleError, "Title");
			}

			command.Slug = command.Slug.Trim().GenerateSlug();
			if (blog?.Slug.Trim() != command.Slug.Trim())
			{
				if (await _blogRepository.Exists(x => x.Slug == command.Slug.Trim()))
					return new(Status.BadRequest, ErrorMessages.DuplicateSlugError, "Slug");
			}

			//check new image choosed or not
			if (command.ImageFile is not null)
			{
				if (!command.ImageFile.IsImage())
					return new(Status.BadRequest, ErrorMessages.IsNotImage, "ImageFile");

				//store image in database and return imageName
				var imageName = _fileService.UploadFileAndReturnFileName(command.ImageFile, Directories.BlogImageDirectory);

				if (imageName is null)
					return new(Status.InternalServerError, ErrorMessages.InternalServerError);

				command.ImageName = imageName;

				_fileService.ResizeImage(imageName, Directories.BlogCategoryImageDirectory, 100);
				_fileService.ResizeImage(imageName, Directories.BlogCategoryImageDirectory, 400);

				var ok = await _blogRepository.Edit(command);

				if (!ok)
				{

					//Delete Images That Uploaded
					_fileService.DeleteFile(imageName, Directories.BlogImageDirectory);
					_fileService.DeleteFile(imageName, Directories.BlogImageDirectory100);
					_fileService.DeleteFile(imageName, Directories.BlogImageDirectory400);

					return new(Status.InternalServerError, ErrorMessages.InternalServerError);
				}

				return new(Status.Success);

			}

			//if image was not changed no need to store any image and old image stayed 

			if (!await _blogRepository.Edit(command))
				return new(Status.InternalServerError, ErrorMessages.InternalServerError);

			return new(Status.Success);

		}

		public async Task<EditBlog?> GetForEdit(long id)
		    => await _blogRepository.GetForEdit(id);
		
		public async Task<bool> ActivationChange(long blogId) 
			=> await _blogRepository.ActivationChange(blogId);

		public async Task<bool> SpecialationChange(long blogId)
			=> await _blogRepository.SpecialationChange(blogId);

		public async Task<bool> IncreaseVisits(string slug)
			=>await _blogRepository.IncreaseVisits(slug);



	}
}
