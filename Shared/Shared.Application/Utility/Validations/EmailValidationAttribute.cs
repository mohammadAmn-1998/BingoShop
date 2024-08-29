using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Application.Utility.Validations
{
	public class EmailValidationAttribute : ValidationAttribute
	{
		public override bool IsValid(object? value)
		{
			if(value == null) return true;

			var email = value as string;

			var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
			return regex.IsMatch(email!);

		}
	}
}
