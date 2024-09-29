using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Utility
{
	public class BasePagination
	{

		public int EntityCount { get; set; }

		public int Take { get; set; }

		public int TotalPages { get; set; }

		public int StartPage { get; set; }

		public int CurrentPage { get; set; }

		public int EndPage { get; set; }

		public int Skip { get; set; }

		public void GetBasePagination(IQueryable<object>? data, int pageId, int take)
		{

			EntityCount = data?.Count() ?? 0;

			Take = take;

			TotalPages = (int)(Math.Ceiling(EntityCount / (double)take));

			CurrentPage = pageId;

			StartPage = (((CurrentPage - 3) < 0 || (CurrentPage - 3) == 0) ? 1 : (CurrentPage - 3));

			EndPage = ((CurrentPage + 3) > TotalPages ? TotalPages : (CurrentPage + 3));

			Skip = (pageId - 1) * take;

		}

	}
}
