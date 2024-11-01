﻿using Shared.Application;
using Shared.Application.Services;
using Shared;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Site.Application.Contract.SiteSettingApplication.Command;
using Site.Domain.SiteSettingAgg;

namespace Site.Application.Services;

internal class SiteSettingApplication : ISiteSettingApplication
{
    private readonly ISiteSettingRepository _siteSettingRepository;
    private readonly IFileService _fileService;

    public SiteSettingApplication(ISiteSettingRepository siteSettingRepository, IFileService fileService)
    {
        _siteSettingRepository = siteSettingRepository;
        _fileService = fileService;
    }

    public async Task<UbsertSiteSetting> GetForUbsert() =>
        await _siteSettingRepository.GetForUbsert();

    public async Task<OperationResult> Ubsert(UbsertSiteSetting command)
    {
        var site = await _siteSettingRepository.GetSingle();
        if (site == null)
	        return new(Status.BadRequest, ErrorMessages.BadRequestError);
        var logoName = site.LogoName;
        var oldLogoName = site.LogoName;
        if (!string.IsNullOrEmpty(command.LogoAlt))
	        command.LogoAlt = command.LogoAlt.Trim().Replace(" ", "_");
	        
        if (command.LogoFile != null)
        {
            if (!command.LogoFile.IsImage()) return new(Status.BadRequest, ErrorMessages.IsNotImage, nameof(command.LogoFile));
            logoName = _fileService.UploadFileAndReturnFileName(command.LogoFile, Directories.SiteSettingImageDirectory);
            if (logoName == null)
                return new(Status.BadRequest, ErrorMessages.IsNotImage, nameof(command.LogoFile));
            _fileService.ResizeImage(logoName, Directories.SiteSettingImageDirectory, 300);
            _fileService.ResizeImage(logoName, Directories.SiteSettingImageDirectory, 100);
            _fileService.ResizeImage(logoName, Directories.SiteSettingImageDirectory, 400);
        }
        var favIconName = site.FavIcon;
        var oldfavIconName = site.FavIcon;
        if (command.FavIconFile != null)
        {
            if (!command.FavIconFile.IsImage()) return new(Status.BadRequest, ErrorMessages.IsNotImage, nameof(command.FavIconFile));
            favIconName = _fileService.UploadFileAndReturnFileName(command.FavIconFile, Directories.SiteSettingImageDirectory);
			if(favIconName == null)
				return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(command.FavIconFile));
			if (logoName == null)
                return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(command.FavIconFile));
            _fileService.ResizeImage( Directories.SiteSettingImageDirectory,favIconName, 64);
            _fileService.ResizeImage(favIconName, Directories.SiteSettingImageDirectory, 32);
            _fileService.ResizeImage( favIconName, Directories.SiteSettingImageDirectory,16);
        }
        site.Edit(command.Instagram, command.WhatsApp, command.Telegram, command.Youtube, logoName,
            command.LogoAlt, favIconName, command.Enamad, command.SamanDehi, command.SeoBox,
            command.Android, command
            .IOS, command.FooterDescription, command.FooterTitle, command.Phone1, command.Phone2,
            command.Email1, command.Email2, command.Address, command.ContactDescription, command.AboutDescription, command.AboutTitle);
        if (await _siteSettingRepository.Edit(site))
        {

            return new(Status.Success);
        }
        else
        {
            if (command.LogoFile != null)
            {
	            _fileService.DeleteFile(logoName,Directories.SiteSettingImageDirectory );
	            _fileService.DeleteFile(logoName, Directories.SiteSettingImageDirectory300);

            }
            if (command.FavIconFile != null)
            {
	            _fileService.DeleteFile(favIconName, Directories.SiteSettingImageDirectory);
	            _fileService.DeleteFile(favIconName, Directories.SiteSettingImageDirectory16);
	            _fileService.DeleteFile(favIconName, Directories.SiteSettingImageDirectory32);
	            _fileService.DeleteFile(favIconName, Directories.SiteSettingImageDirectory64);
            }
            return new(Status.InternalServerError,ErrorMessages.InternalServerError,nameof(command.Instagram));
        }
    }
}