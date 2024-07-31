using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Shared.Application.Utility.Validations;

public class PasswordValidationAttribute : ValidationAttribute
{
	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
			
		var password = value!.ToString()!;

		// Check if password contains both letters and numbers
		if (!Regex.IsMatch(password, @"[A-Za-z]") || !Regex.IsMatch(password, @"\d"))
		{
			return new ValidationResult(ErrorMessages.PasswordMustContainNumbersAndLetters);
		}

		return ValidationResult.Success;
	}
}