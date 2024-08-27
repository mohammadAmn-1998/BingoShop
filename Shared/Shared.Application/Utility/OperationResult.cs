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

		#region Error

		public static OperationResult Error()
		{
			return new(Status.Error);
		}
		public static OperationResult Error(string message)
		{
			return new(Status.Error, message);
		}

		#endregion

		#region Success

		public static OperationResult Success()
		{
			return new(Status.Success);
		}
		public static OperationResult Success(string message)
		{
			return new(Status.Success, message);
		}

		#endregion

		#region NotFound

		public static OperationResult NotFound()
		{
			return new(Status.NotFound);
		}
		public static OperationResult NotFound(string message)
		{
			return new(Status.NotFound, message);
		}

		#endregion

	}
}
