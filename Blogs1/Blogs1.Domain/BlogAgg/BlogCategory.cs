using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.SeedWorks.Base;

namespace Blogs1.Domain.BlogAgg
{
	public class BlogCategory : BaseEntity<long>
	{

		public string Title { get; private set; }

		public string ImageName { get; private set; }

		public string ImageAlt { get; private set; }

		public long ParentId { get; private set; }

		public string Slug { get; private set; }


		public DateTime? UpdateDate { get; private set; }

		public BlogCategory(string title, string imageName, long parentId, string slug, string imageAlt)
		{
			Title = title;
			ImageName = imageName;
			ParentId = parentId;
			Slug = slug;
			ImageAlt = imageAlt;
			CreateDate = DateTime.Now;
		}

		public void Edit(string title, string imageName, string slug, string imageAlt,long parentId)
		{
			Title = title;
			ImageName = imageName;
			Slug = slug;
			UpdateDate = DateTime.Now;
			ImageAlt = imageAlt;
			ParentId = parentId;
		}

	}
}
