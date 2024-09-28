using PostModule.Application.Contract.UserPostApplication.Command;
using PostModule.Domain.UserPostAgg;
using Shared;
using Shared.Application;
using Shared.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace PostModule.Application.Services
{
    internal class PackageApplication : IPackageApplication
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IFileService _fileService;
        public PackageApplication(IPackageRepository packageRepository,IFileService fileService)
        {
            _packageRepository = packageRepository;
            _fileService = fileService;
        }

        public bool ActivationChange(int id)
        {
            var package = _packageRepository.GetById(id);
            package.ActivationChange();
            return _packageRepository.Save();
        }

        public OperationResult Create(CreatePackage command)
        {
            if (_packageRepository.IsExists(p => p.Title.Trim() == command.Title.Trim()))
                return new(Status.BadRequest, ErrorMessages.DuplicateTitleError, nameof(command.Title));

            if (command.ImageFile == null || !command.ImageFile.IsImage())
                return new(Status.BadRequest, ErrorMessages.IsNotImage, nameof(command.ImageFile));

            var imageName = _fileService.UploadFileAndReturnFileName(command.ImageFile, Directories.PackageImageDirectory);
            if (imageName == null)
                return new(Status.InternalServerError, ErrorMessages.InternalServerError, "Title");

            _fileService.ResizeImage(imageName, Directories.PackageImageDirectory, 400);
            _fileService.ResizeImage(imageName, Directories.PackageImageDirectory, 100);


            Package package = new(command.Title, command.Description, command.Count, command.Price,imageName,command.ImageAlt);
            if (_packageRepository.Insert(package))
            {
                return new(Status.Success);
            }

            _fileService.DeleteFile(imageName, Directories.PackageImageDirectory);
            _fileService.DeleteFile(imageName, Directories.PackageImageDirectory100);
            _fileService.DeleteFile(imageName, Directories.PackageImageDirectory400);
            return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(command.Title));
        }

        public OperationResult Edit(EditPackage command)
        {
            var package = _packageRepository.GetById(command.Id);
            if (_packageRepository.IsExists(p => p.Title.Trim() == command.Title.Trim() && p.Id != package.Id))
                return new(Status.BadRequest, ErrorMessages.DuplicateTitleError, nameof(command.Title));
            var imageName = command.ImageName;
            string oldImageName = command.ImageName;
            if (command.ImageFile != null)
            {
                imageName = _fileService.UploadFileAndReturnFileName(command.ImageFile, Directories.PackageImageDirectory);
                if (imageName == null)
                    return new(Status.InternalServerError, ErrorMessages.InternalServerError, "Title");
                _fileService.ResizeImage(imageName, Directories.PackageImageDirectory, 400);
                _fileService.ResizeImage(imageName, Directories.PackageImageDirectory, 100);
            }
            package.Edit(command.Title, command.Description, command.Count, command.Price,imageName,command.ImageAlt);
            if (_packageRepository.Save())
            {
	            return new(Status.Success);
            }
            if (command.ImageFile != null)
            {
                _fileService.DeleteFile(imageName,Directories.PackageImageDirectory);
                _fileService.DeleteFile(imageName,Directories.PackageImageDirectory100);
                _fileService.DeleteFile(imageName,Directories.PackageImageDirectory400);
                
            }
            return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(command.Title));
        }

        public EditPackage GetForEdit(int id) =>
            _packageRepository.GetForEdit(id);
    }
}
