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
		private readonly IBlogRepository _blogRepository;
		private readonly IFileService _fileService;

		public BlogService(IBlogRepository blogRepository, IFileService fileService)
		{
			_blogRepository = blogRepository;
			_fileService = fileService;
		}

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
			throw new NotImplementedException();
		}

		public async Task<bool> ActivationChange(long blogId) 
			=> await _blogRepository.ActivationChange(blogId);

		public async Task<bool> SpecialationChange(long blogId)
			=> await _blogRepository.SpecialationChange(blogId);

	}
}
