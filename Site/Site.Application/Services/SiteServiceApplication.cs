using Shared.Application;
using Shared.Application.Services;
using Site.Application.Contract.SiteServiceApplication.Command;
using Site.Domain.SiteServiceAgg;
using Shared;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Site.Domain.BanerAgg;
using System.Windows.Input;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Site.Application.Services;

internal class SiteServiceApplication : ISiteServiceApplication
{
    private readonly ISiteServiceRepository _siteServiceepository;
    private readonly IFileService _fileService;

    public SiteServiceApplication(ISiteServiceRepository siteServiceepository, IFileService fileService)
    {
        _siteServiceepository = siteServiceepository;
        _fileService = fileService;
    }

    public async Task<bool> ActivationChange(long id)
    => await _siteServiceepository.ChangeActivation(id);

    public async Task<OperationResult> Create(CreateSiteService commmand)
    {
        if (commmand.ImageFile == null || !commmand.ImageFile.IsImage())
            return new(Status.BadRequest, ErrorMessages.IsNotImage, nameof(commmand.ImageFile));

        var imageName = _fileService.UploadFileAndReturnFileName(commmand.ImageFile, Directories.ServiceImageDirectory);
        if (imageName == null) 
            return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(commmand.ImageFile));

        _fileService.ResizeImage(imageName, Directories.ServiceImageDirectory, 100);
        _fileService.ResizeImage(imageName, Directories.ServiceImageDirectory, 400);
        SiteService service = new(imageName, commmand.ImageAlt, commmand.Title);
        if (await _siteServiceepository.Create(service))
            return new(Status.Success);

        _fileService.DeleteFile(imageName, Directories.ServiceImageDirectory);
        _fileService.DeleteFile(imageName, Directories.ServiceImageDirectory100);
        _fileService.DeleteFile(imageName, Directories.ServiceImageDirectory400);

        return new(Status.InternalServerError, ErrorMessages.InternalServerError);
    }

    public async Task<OperationResult> Edit(EditSiteService commmand)
    {
        var service =await _siteServiceepository.GetById(commmand.Id);
        var imageName = service.ImageName;
        string oldImageName = service.ImageName;
        if (commmand.ImageFile != null)
        {
            if (!commmand.ImageFile.IsImage()) return new(Status.BadRequest, ErrorMessages.IsNotImage, nameof(commmand.ImageFile));
            imageName = _fileService.UploadFileAndReturnFileName(commmand.ImageFile, Directories.ServiceImageDirectory);
            if (imageName == null)
                return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(commmand.ImageFile));
            _fileService.ResizeImage(imageName, Directories.ServiceImageDirectory, 100);
            _fileService.ResizeImage(imageName, Directories.ServiceImageDirectory, 400);
        }
        service.Edit(imageName, commmand.ImageAlt, commmand.Title);
        if (await _siteServiceepository.Edit(service))
        {
           
            return new(Status.Success);
        }
        else
        {

            if (commmand.ImageFile != null)
            {
	            _fileService.DeleteFile(imageName, Directories.ServiceImageDirectory);
	            _fileService.DeleteFile(imageName, Directories.ServiceImageDirectory100);
	            _fileService.DeleteFile(imageName, Directories.ServiceImageDirectory400);
            }
            return new(Status.InternalServerError, ErrorMessages.InternalServerError);
        }

    }

    public EditSiteService? GetForEdit(long id) =>
        _siteServiceepository.GetForEdit(id);
}
