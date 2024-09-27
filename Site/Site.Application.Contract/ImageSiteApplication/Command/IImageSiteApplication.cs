using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;

namespace Site.Application.Contract.ImageSiteApplication.Command
{
	public interface IImageSiteApplication
	{
		Task<OperationResult> Create(CreateImageSite command);
		
	}
}
