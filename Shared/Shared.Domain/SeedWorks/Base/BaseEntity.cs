using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.SeedWorks.Base
{
	public class BaseEntity<TKey>
	{

		public TKey Id { get; set; }

		public DateTime CreateDate { get; set; } = DateTime.Now;

		public DateTime? UpdateDate { get; set; }

		public bool Active { get; set; }

		

		public void ActivationChange()
		{
			Active = !Active;
		}
	}
}
