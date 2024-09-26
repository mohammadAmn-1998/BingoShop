using Shared.Domain.SeedWorks.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs1.Domain.BlogAgg
{
	public class Blog : BaseEntityUpdateActive<long>
	{

		public string Title { get; private set; }

		public string ImageName { get; private set; }

		public string ImageAlt { get; private set; }

		public long UserId { get; private set; }

		public string Author { get; private set; }

		public long CategoryId { get; private set; }

		public long SubCategoryId { get; private set; }

		public string Slug { get; private set; }

		public string Summary { get; private set; }

		public string Content { get; private set; }

		public long TotalVisits { get; private set; }

		public bool IsSpecial { get; private set; }

		public long Likes { get; private set; }

		public long Dislikes { get; private set; }


		public Blog(string title, string imageName, string imageAlt, long userId, string author, long categoryId, long subCategoryId, string slug, string summary, string content, bool isSpecial)
		{
			Title = title;
			ImageName = imageName;
			ImageAlt = imageAlt;
			UserId = userId;
			Author = author;
			CategoryId = categoryId;
			SubCategoryId = subCategoryId;
			Slug = slug;
			Summary = summary;
			Content = content;
			IsSpecial = isSpecial;
			CreateDate = DateTime.Now;
		}

		public void Edit(string title, string imageName, string imageAlt, long categoryId, long subCategoryId, string slug, string summary, string content, bool isSpecial)
		{
			Title = title;
			ImageName = imageName;
			ImageAlt = imageAlt;
			CategoryId = categoryId;
			SubCategoryId = subCategoryId;
			Slug = slug;
			Summary = summary;
			Content = content;
			IsSpecial = isSpecial;
			UpdateDate = DateTime.Now;
		}

		public void IncreaseLikes()
		{
			Likes = Likes + 1;
		}

		public void IncreaseDislikes()
		{
			Dislikes = Dislikes + 1;
		}

		public void ChangeSpecialation()
		{
			IsSpecial = !IsSpecial;
		}

		public void IncreaseTotalVisits()
		{
			TotalVisits = TotalVisits + 1;
		}
	}
}
