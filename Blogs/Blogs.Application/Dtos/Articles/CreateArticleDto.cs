using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Blogs.Application.Dtos.Articles
{
	public class CreateArticleDto :EditArticleDto
	{

		public int UserId { get; set; }
		public string Author { get; set; }

	}

	public class EditArticleDto 
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string ImageName { get; set; }

		public string ImageAlt { get; set; }

		public IFormFile? ImageFile { get; set; }

		public int CategoryId { get; set; }

		public int SubCategoryId { get; set; }

		public string Slug { get; set; }

		public string Summary { get; set; }

		public string Content { get; set; }

		public bool IsSpecial { get; set; }


	}
}
