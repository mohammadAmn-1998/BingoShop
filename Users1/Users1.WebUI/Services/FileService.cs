using Shared.Application.Services;
using Shared.Application.Utility;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Users1.WebUI.Services
{
	public class FileService : IFileService
	{

		private readonly IWebHostEnvironment _environment;

		public FileService(IWebHostEnvironment environment)
		{
			_environment = environment;
		}

		public string? UploadFileAndReturnFileName(IFormFile file, string folder)
		{
			try
			{
				var extension = Path.GetExtension(file.Name);
				var fileName = Guid.NewGuid() + extension;
				var filePath = Path.Combine(_environment.WebRootPath, Path.Combine(folder, fileName));

				using var str = File.Create(filePath);

				file.CopyTo(str);
				return fileName;


			}
			catch (Exception e)
			{
				return null;
			}
		}

		public bool ResizeImage(string imageName, string folder, int newSize)
		{
			var imageFilePath = Path.Combine(_environment.WebRootPath, folder) + $"/{imageName}";
			var newDirectory = Path.Combine(_environment.WebRootPath, folder) + $"/{newSize}";

			if (!Directory.Exists(newDirectory))
				Directory.CreateDirectory(newDirectory);

			try
			{
				using var image = Image.Load(imageFilePath);
				image.Mutate(x => x.Resize(newSize, 0));

				image.Save(newDirectory + "/" + imageName);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool DeleteFile(string fileName, string folder)
		{
			try
			{
				var filePath = Path.Combine(_environment.WebRootPath, Path.Combine(folder, fileName));
				File.Delete(filePath);
				return true;

			}
			catch (Exception e)
			{
				return false;
			}
		}
	}
}
