using Shared.Application;
using Site.Application.Contract.ImageSiteApplication.Query;
using Site.Domain.SiteImageAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Utility;
using Shared.Domain.SeedWorks.Base;
using Site.Infrastructure;

namespace Site.Query.Services
{
	internal class ImageSiteQuery :BaseRepository, IImageSiteQuery
	{

		public ImageSiteQuery(SiteContext context) : base(context)
		{
		}


		public ImageAdminPaging GetAllForAdmin(int pageId, int take, string filter)
		{
			IQueryable<SiteImage> result;
			if (!string.IsNullOrEmpty(filter))
				result = Table<SiteImage>().Where(c => c.Title.Contains(filter));
			else
				result = Table<SiteImage>();

			ImageAdminPaging model = new();
			model.GetBasePagination(result, pageId, take);
			var skip = (pageId -1) * take;
			model.Filter = filter;
			model.Images = new();
			if(result.Count() > 0)
				model.Images =  result.Skip(skip).Take(model.Take).Select(s => new ImageSiteAdminQueryModel
				{
					CreateDate = s.CreateDate.ConvertToPersianDate(),
					Id = s.Id,
					ImageName =  s.ImageName,
					Title = s.Title
				}).ToList();

			return model;
		}

		
	}
}
