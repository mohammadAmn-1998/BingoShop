using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Utility
{
	public static class IranMobileNumberValidator
	{

		public static string ChangeToPersianMobileNumber(this string mobileNumber)
		{
			if (mobileNumber.StartsWith("+989"))
				return mobileNumber.Replace("+98", "0");

			//otherwise because we checked mobile number validation by MobileValidation Attribute class and
			//just need to change all mobile numbers to this pattern to store in data base correctly 
			// correct pattern : 09xxxxxxxxx;
				return mobileNumber;

		}

	}
}
