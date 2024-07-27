using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Dtos.BlogCategories
{
	public class CreateBlogCategoryDto : EditBlogCategoryDto
	{

		public int ParentId { get; set; }
	}
}
