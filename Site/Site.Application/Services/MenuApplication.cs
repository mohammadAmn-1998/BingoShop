using Shared.Application;
using Shared.Application.Services;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Site.Application.Contract.MenuApplication.Command;
using Site.Domain.MenuAgg;


namespace Site.Application.Services
{
    internal class MenuApplication : IMenuApplication
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IFileService _fileService;

        public MenuApplication(IMenuRepository menuRepository, IFileService fileService)
        {
            _menuRepository = menuRepository;
            _fileService = fileService;
        }

        public async Task<bool> ActivationChange(long id)
       => await _menuRepository.ActivationChange(id);

        public async Task<OperationResult> Create(CreateMenu command)
        {
            if(command.Status == Shared.Domain.Enums.MenuStatus.منوی_اصلی_با_زیر_منو)
            {
                if(command.ImageFile == null || !command.ImageFile.IsImage())
                    return new(Status.BadRequest, $"{MenuStatus.منوی_اصلی_با_زیر_منو.ToString().Replace("_"," ")} نیاز به یک تصویر دارد", nameof(command.ImageFile));
                else if(string.IsNullOrEmpty(command.ImageAlt))
                    return new(Status.BadRequest, ErrorMessages.FieldIsRequired, nameof(command.ImageAlt));
            }
            else
            {
                if(command.ImageFile != null)
                    return new(Status.BadRequest, $"{MenuStatus.منوی_اصلی_با_زیر_منو.ToString().Replace("_", " ")} نیاز به تصویر ندارد", nameof(command.ImageFile));
                if(!string.IsNullOrEmpty(command.ImageAlt))
                    return new(Status.BadRequest, $"{MenuStatus.منوی_اصلی_با_زیر_منو.ToString().Replace("_", " ")} نیاز به Alt تصویر ندارد", nameof(command.ImageAlt));
            }
            var imageName = "";
            if(command.ImageFile != null)
            {
                 imageName = _fileService.UploadFileAndReturnFileName(command.ImageFile, Directories.MenuImageDirectory);
                if (imageName == null)
                    return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(command.ImageFile));

                _fileService.ResizeImage(imageName, Directories.MenuImageDirectory, 100);
                _fileService.ResizeImage(imageName, Directories.MenuImageDirectory, 400);
            }

            command.ImageName = imageName;
            Menu menu = new(command.Number, command.Title, command.Url, command.Status, imageName, command.ImageAlt, null);
            if (await _menuRepository.Create(menu))
                return new(Status.Success);
            

            return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(command.ImageAlt));
        }

        public async Task<OperationResult> CreateSub(CreateSubMenu command)
        {
            if(command.ParentStatus == MenuStatus.منوی_وبلاگ_با_زیر_منوی_عکس_دار)
            {
                if (command.ImageFile == null || !command.ImageFile.IsImage())
                    return new(Status.BadRequest, $"{MenuStatus.منوی_وبلاگ_با_زیر_منوی_عکس_دار.ToString().Replace("_", " ")} نیاز به یک تصویر دارد", nameof(command.ImageFile));
                else if (string.IsNullOrEmpty(command.ImageAlt))
                    return new(Status.BadRequest, ErrorMessages.FieldIsRequired, nameof(command.ImageAlt));
            }
            else
            {
                if (command.ImageFile != null)
                    return new(Status.BadRequest, " نیاز به تصویر ندارد", nameof(command.ImageFile));
                if (!string.IsNullOrEmpty(command.ImageAlt))
                    return new(Status.BadRequest, " نیاز به Alt تصویر ندارد", nameof(command.ImageAlt));
            }

            var imageName = "";
            if (command.ImageFile != null)
            {
                imageName = _fileService.UploadFileAndReturnFileName(command.ImageFile, Directories.MenuImageDirectory);
                if (imageName == null) 
                    return new(Status.BadRequest, ErrorMessages.InternalServerError, nameof(command.ImageFile));

                _fileService.ResizeImage(imageName, Directories.MenuImageDirectory, 100);
                _fileService.ResizeImage(imageName, Directories.MenuImageDirectory, 400);
            }
            MenuStatus status = MenuStatus.منوی_اصلی;
            switch (command.ParentStatus)
            {
                case MenuStatus.منوی_اصلی_با_زیر_منو:
                    status = MenuStatus.زیرمنوی_سردسته;
                    break;
                case MenuStatus.زیرمنوی_سردسته:
                    status = MenuStatus.زیرمنو;
                    break;
                case MenuStatus.تیتر_منوی_فوتر:
                    status = MenuStatus.منوی_فوتر;
                    break;
                case MenuStatus.منوی_وبلاگ_با_زیرمنوی_بدون_عکس:
                    status = MenuStatus.زیر_منوی_وبلاگ;
                    break;
                case MenuStatus.منوی_وبلاگ_با_زیر_منوی_عکس_دار:
                    status = MenuStatus.زیر_منوی_وبلاگ;
                    break;
                default:
                    return new(Status.InternalServerError,ErrorMessages.InternalServerError, nameof(command.Title));
            }

            command.ImageName = imageName;
            Menu menu = new(command.Number, command.Title, command.Url, status, imageName, command.ImageAlt, command.ParentId);
            if (await _menuRepository.Create(menu))
                return new(Status.Success);
           
            return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(command.ImageAlt));
        }

        public async Task<OperationResult> Edit(EditMenu command)
        {
            var menu =await _menuRepository.GetById(command.Id);
            if (menu is null)
	            return new(Status.BadRequest, ErrorMessages.BadRequestError);
            var imageName = command.ImageName;
            var oldImageName = command.ImageName;
            if (command.ImageFile != null && string.IsNullOrEmpty(command.ImageAlt))
                return new(Status.BadRequest, ErrorMessages.FieldIsRequired, nameof(command.ImageAlt));
            if (command.ImageFile != null && !command.ImageFile.IsImage())
                return new(Status.BadRequest, ErrorMessages.IsNotImage, nameof(command.ImageFile));
            if (command.ImageFile != null)
            {
                imageName = _fileService.UploadFileAndReturnFileName(command.ImageFile, Directories.MenuImageDirectory);
                if (imageName == null)
                    return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(command.ImageFile));

                _fileService.ResizeImage(imageName, Directories.MenuImageDirectory, 100);
                _fileService.ResizeImage(imageName, Directories.MenuImageDirectory, 400);
            }
            menu.Edit(command.Number, command.Title, command.Url, imageName, command.ImageAlt);

            if (await _menuRepository.Edit(menu))
            {
               
                return new(Status.Success);
            }
          
            return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(command.ImageAlt));
        }

        public async Task<CreateSubMenu> GetForCreate(long parentId)
        {
            var parent =await _menuRepository.GetById(parentId);
            return new()
            {
                ImageAlt = "",
                ImageFile = null,
                ParentId = parent!.Id,
                ParentStatus = parent.Status,
                ParentTitle = $"افزودن زیر منو برای {parent.Title}"
                
            };
        }

        public EditMenu? GetForEdit(long id) =>
            _menuRepository.GetForEdit(id);
    }
}
