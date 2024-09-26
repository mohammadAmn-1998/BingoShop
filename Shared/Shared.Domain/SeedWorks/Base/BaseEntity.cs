using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.SeedWorks.Base
{

	public class BaseEntity<TKey>
	{

		public TKey Id { get; private set; }

		public DateTime CreateDate { get; set; }

		public BaseEntity()
		{
			CreateDate = DateTime.Now;
		}
		
	}

	public class BaseEntityUpdate<TKey> : BaseEntity<TKey>
	{

		public DateTime UpdateDate { get; set; }

		public BaseEntityUpdate()
		{
			UpdateDate = DateTime.Now;
		}

		 public void UpdateEntity()
        {
            UpdateDate = DateTime.Now;
        }

	}

	public class BaseEntityActive<TKey> : BaseEntity<TKey>
	{

		public bool Active { get;  set; }

		public BaseEntityActive()
		{
			Active = true;
		}

		public void ActivationChange()
		{
			Active = !Active;
		}
	}

	public class BaseEntityUpdateActive<TKey> : BaseEntityActive<TKey>
	{

		public DateTime? UpdateDate { get; set; }

		public BaseEntityUpdateActive()
		{
			UpdateDate = DateTime.Now;
		}

	}
}
