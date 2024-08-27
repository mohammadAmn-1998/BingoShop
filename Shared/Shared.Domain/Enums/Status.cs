using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Enums
{
	public enum Status
	{

		
		NotFound = 404,
		Forbidden = 401,
		InternalServerError = 500,
		BadRequest = 400,
		UnAuthorized = 403,
		Success = 200,
		Error = 600,
		Info = 10,

	}
}
