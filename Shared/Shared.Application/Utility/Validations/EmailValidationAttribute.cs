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

			const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
			return Regex.IsMatch(email!, pattern);

		}
	}
}
