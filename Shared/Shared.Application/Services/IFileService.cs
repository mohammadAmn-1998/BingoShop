using System;
using Microsoft.AspNetCore.Http;

namespace Shared.Application.Services;

public interface IFileService
{
	string? UploadFileAndReturnFileName(IFormFile file, string folder);

	bool ResizeImage(string imageName, string folder, int newSize);

	bool DeleteFile(string fileName,string folder);

}