using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Blogs.Application.Dtos.BlogCategories
{
	public class  EditBlogCategoryDto
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public IFormFile? ImageFile { get; set; }

		public string? ImageName { get; set; }

		public string ImageAlt { get; set; }

		
		public string Slug { get; set; }

	}
}
