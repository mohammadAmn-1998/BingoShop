using Shared.Application;
using Shared.Application.Services;
using Shared;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Site.Application.Contract.SliderApplication.Command;
using Site.Domain.SliderAgg;

namespace Site.Application.Services
{
    internal class SliderApplication : ISliderApplication
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IFileService _fileService;

        public SliderApplication(ISliderRepository sliderRepository, IFileService fileService)
        {
            _sliderRepository = sliderRepository;
            _fileService = fileService;
        }

        public async Task<bool> ActivationChange(long id)
        => await _sliderRepository.ChangeActivation(id);

        public async Task<OperationResult> Create(CreateSlider command)
        {
            if (command.ImageFile == null || !command.ImageFile.IsImage())
                return new(Status.BadRequest, ErrorMessages.IsNotImage, nameof(command.ImageFile));

            var imageName = _fileService.UploadFileAndReturnFileName(command.ImageFile, Directories.SliderImageDirectory);
            if (imageName == null)
                return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(command.ImageFile));

            _fileService.ResizeImage(imageName, Directories.SliderImageDirectory, 100);
            Slider slider = new(imageName, command.ImageAlt,command.Url);
            if (await _sliderRepository.Create(slider)) return new(Status.Success);


            _fileService.DeleteFile(imageName, Directories.SliderImageDirectory);
            _fileService.DeleteFile(imageName, Directories.SliderImageDirectory100);
            return new(Status.InternalServerError, ErrorMessages.InternalServerError);
        }

        public async Task<OperationResult> Edit(EditSlider command)
        {
            var slider =await _sliderRepository.GetById(command.Id);
            var imageName = slider.ImageName;
            string oldImageName = slider.ImageName;
            if (command.ImageFile != null)
            {
                if (!command.ImageFile.IsImage()) return new(Status.BadRequest, ErrorMessages.IsNotImage, nameof(command.ImageFile));
                imageName = _fileService.UploadFileAndReturnFileName(command.ImageFile, Directories.SliderImageDirectory);
                if (imageName == null)
                    return new(Status.BadRequest, ErrorMessages.InternalServerError, nameof(command.ImageFile));
                _fileService.ResizeImage(imageName, Directories.SliderImageDirectory, 100);
            }
            slider.Edit(imageName, command.ImageAlt,command.Url);
            if (await _sliderRepository.Edit(slider))
            {
	            return new(Status.Success);
            }
            else
            {

                if (command.ImageFile != null)
                {
	                _fileService.DeleteFile(imageName, Directories.SliderImageDirectory);
	                _fileService.DeleteFile(imageName, Directories.SliderImageDirectory100);
                }
                return new(Status.InternalServerError, ErrorMessages.InternalServerError);
            }
        }

        public EditSlider? GetForEdit(long id) =>
            _sliderRepository.GetForEdit(id);
    }
}
