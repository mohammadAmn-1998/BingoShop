using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Models
{
	public class FilterParams
	{
		public FilterParams(int pageId, int take, string title)
		{
			if (pageId < 1)
			{
				PageId = 1;
			}
			else
			{
				PageId = pageId;
			}

			if (take < 1)
			{
				Take = 10;
			}
			else
			{
				Take = take;
			}
			
			Title = title;
		}

		public FilterParams()
		{
			
		}

		public int PageId { get; set; } = 1;

		[Display(Name="تعداد نمایش در هر صفحه")]
		public int Take { get; set; } = 10;

		public string Title { get; set; } = "";

	}
}
