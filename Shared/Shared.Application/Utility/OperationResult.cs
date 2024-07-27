using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Enums;

namespace Shared.Application.Utility
{
	public  class OperationResult
	{

		public Status Status { get; set; }

		public string? ModelName { get; set; }

		public string? Message { get; set; }

		public OperationResult(Status status, string? message = "", string? modelName = "")
		{
			Status = status;
			ModelName = modelName;
			Message = message;
		}

		
	}
}
