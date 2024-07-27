using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shared.Application.Utility
{
	public static class FileSecurity
	{

		public static bool IsImage(this IFormFile file)
		{

			try
			{

				using var imageFile = System.Drawing.Image.FromStream(file.OpenReadStream());
				return true;

			}
			catch (Exception e)
			{
				return false;
			}

		}

	}
}
