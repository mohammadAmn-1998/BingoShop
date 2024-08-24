using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Models
{
	public class FilterParams
	{

		public int PageId { get; set; } = 1;

		public int Take { get; set; } = 2;

		public string Title { get; set; } = "";

	}
}
