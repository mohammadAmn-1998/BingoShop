using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.SeedWorks.Base;

namespace Site.Domain.SiteImageAgg
{
	public class SiteImage : BaseEntityActive<long>
	{
		private void SetValues(string imageName, string title)
		{
			ImageName = imageName;
			Title = title;
		}
        public SiteImage(string imageName, string title)
        {
			SetValues(imageName, title);   
        }
        public string ImageName { get; private set; }
        public string Title { get; private set; }
    }
}
