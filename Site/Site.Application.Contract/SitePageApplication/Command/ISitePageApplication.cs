using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;

namespace Site.Application.Contract.SitePageApplication.Command
{
	public interface ISitePageApplication
	{
		Task<OperationResult> Create(CreateSitePage command);
		Task<OperationResult> Edit(EditSitePage command);
		EditSitePage? GetForEdit(long id);
		Task<bool> ActivationChange(long id);
	}
}
