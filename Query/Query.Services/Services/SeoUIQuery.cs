using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Query.Contract.Ui.Seo;
using Seos.Domain;
using Seos.Infrastructure;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;

namespace Query.Services.Services
{
	internal class SeoUIQuery : BaseRepository ,ISeoUIQuery
	{
		public SeoUIQuery(Seo_Context context) : base(context)
		{
		}

		public async Task<SeoUIQueryModel> GetSeoForUI(long ownerId, WhereSeo where,string title)
		{
			try
			{
				var seo = Table<Seo>().FirstOrDefault(x => x.OwnerId == ownerId && x.Where == where);

				if (seo == null)
					return new(where, ownerId, title, "", "", true, "", "");
					

				return new(seo.Where, seo.OwnerId, seo.MetaTitle, seo.MetaDescription, seo.MetaKeyWords, seo.IndexPage,
					seo.Canonical, seo.Schema);
				
			}
			catch (Exception e)
			{
				return new();
			}
		}

		
	}
}
