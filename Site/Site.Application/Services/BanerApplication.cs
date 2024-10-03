using Shared;
using Shared.Application;
using Shared.Application.Services;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Site.Application.Contract.BanerApplication.Command;
using Site.Domain.BanerAgg;


namespace Site.Application.Services
{
    internal class BanerApplication : IBanerApplication
    {
        private readonly IBanerRepository _banerRepository;
        private readonly IFileService _fileService;

        public BanerApplication(IBanerRepository banerRepository, IFileService fileService)
        {
            _banerRepository = banerRepository;
            _fileService = fileService;
        }

        public async Task<bool> ActivationChange(long id)
        =>await _banerRepository.ChangeActivation(id);
           
        

        public async Task<OperationResult> Create(CreateBaner command)
        {
            if (command.ImageFile == null || !command.ImageFile.IsImage())
                return new(Status.BadRequest, ErrorMessages.IsNotImage, nameof(command.ImageFile));

            var imageName =  _fileService.UploadFileAndReturnFileName(command.ImageFile, Directories.BanerImageDirectory);
            if (imageName == null)
                return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(command.ImageFile));

            command.ImageName = imageName;

            _fileService.ResizeImage(imageName, Directories.BanerImageDirectory, 100);

            if ( await _banerRepository.Create(command))
                return new(Status.Success);

            _fileService.DeleteFile(imageName, Directories.BanerImageDirectory);
            _fileService.DeleteFile(imageName, Directories.BanerImageDirectory100);
            _fileService.DeleteFile(imageName, Directories.BanerImageDirectory400);
            return new(Status.InternalServerError,ErrorMessages.InternalServerError);
        }

        public async Task<OperationResult> Edit(EditBaner command)
        {
            var baner = _banerRepository.GetById(command.Id);
            var imageName = baner?.ImageName;
            string oldImageName = baner.ImageName;
            if (command.ImageFile != null)
            {
                if (!command.ImageFile.IsImage())
	                return new(Status.BadRequest, ErrorMessages.IsNotImage, nameof(command.ImageFile));

                imageName = _fileService.UploadFileAndReturnFileName(command.ImageFile, Directories.BanerImageDirectory);
                if (imageName == null)
                    return new(Status.InternalServerError, 
	                    ErrorMessages.InternalServerError, nameof(command.ImageFile));
                command.ImageName = imageName;
                _fileService.ResizeImage(imageName, Directories.BanerImageDirectory, 100);
                _fileService.ResizeImage(imageName, Directories.BanerImageDirectory, 400);
            }
                
                if (await _banerRepository.Edit(command))
                {
                   
                    return new(Status.Success);
                }
                else
                {

	                _fileService.DeleteFile(command.ImageName, Directories.BanerImageDirectory);
	                _fileService.DeleteFile(command.ImageName, Directories.BanerImageDirectory100);
	                _fileService.DeleteFile(command.ImageName, Directories.BanerImageDirectory400);
                    
                    return new(Status.InternalServerError, ErrorMessages.InternalServerError);
                }
            
        }

        public EditBaner? GetForEdit(long id) =>
            _banerRepository.GetForEdit(id);
    }
}
