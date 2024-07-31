using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Utility.Validations
{
	public class MobileValidationAttribute : ValidationAttribute
	{
		public override bool IsValid(object? value)
		{
			if (value is not string num) return false;

			if (string.IsNullOrEmpty(num)) return false;

			if (!num.StartsWith("09") || !num.StartsWith("+989"))
				return false;

			if ((num.StartsWith("09") && num.Length != 11) || (num.StartsWith("+98") && num.Length != 13))
				return false;

			return true;
		}

		
	}
}
