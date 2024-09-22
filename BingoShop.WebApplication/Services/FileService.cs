using Shared.Application.Services;
using Shared.Application.Utility;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BingoShop.WebApplication.Services
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
				var extension = Path.GetExtension(file.FileName);
				var fileName = Guid.NewGuid() + extension;

				if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), folder)))
					Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), folder));

				var filePath = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine(folder, fileName)).Replace("/", "\\");

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
			var imageFilePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), folder), imageName);
			var newDirectory = Path.Combine(Directory.GetCurrentDirectory(), folder) + newSize;

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
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine(folder, fileName));
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
