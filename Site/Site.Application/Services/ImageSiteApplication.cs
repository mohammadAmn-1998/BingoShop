using Shared;
using Shared.Application;
using Shared.Application.Services;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Site.Application.Contract.ImageSiteApplication.Command;
using Site.Domain.SiteImageAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Site.Application.Services;

internal class ImageSiteApplication : IImageSiteApplication
{
	private readonly IImageSiteRepository _imageSiteRepository;
	private readonly IFileService _fileService;

	public ImageSiteApplication(IImageSiteRepository imageSiteRepository, IFileService fileService)
	{
		_imageSiteRepository = imageSiteRepository;
		_fileService = fileService;
	}

	public async Task<OperationResult> Create(CreateImageSite command)
	{
		if(command.ImageFile == null || !command.ImageFile.IsImage())
            return new(Status.BadRequest, ErrorMessages.IsNotImage);
        var imageName = _fileService.UploadFileAndReturnFileName(command.ImageFile, Directories.ImageSiteDirectory);
		if (imageName == null)
			return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(command.ImageFile));
		command.ImageName = imageName;

		_fileService.ResizeImage(imageName, Directories.ImageSiteDirectory, 100);

		if (await _imageSiteRepository.Create(command)) return new(Status.Success);

		_fileService.DeleteFile(command.ImageName, Directories.ImageSiteDirectory);
		_fileService.DeleteFile(command.ImageName, Directories.ImageSiteDirectory100);
		_fileService.DeleteFile(command.ImageName, Directories.ImageSiteDirectory400);
		return new(Status.InternalServerError, ErrorMessages.InternalServerError);
	}

	public async Task<bool> DeleteFromDataBase(int id)
	{
		try
		{
			var imageSite = await _imageSiteRepository.GetById(id);
			if (imageSite == null) throw new NullReferenceException();
			var imageName = imageSite.ImageName;

			if (await _imageSiteRepository.Delete(id))
			{
				_fileService.DeleteFile(imageName, Directories.ImageSiteDirectory);
				_fileService.DeleteFile(imageName, Directories.ImageSiteDirectory100);

				return true;
			}

			return false;
		}
		catch (Exception e)
		{
			return false;
		}
	}
}