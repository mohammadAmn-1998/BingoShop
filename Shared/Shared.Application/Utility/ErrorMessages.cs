using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Utility
{
	public static class ErrorMessages
	{

		#region Common

		public const string DuplicateError = "این فیلد تکراری است";
		public const string FieldIsRequired = "این فیلد اجباری است";
		public const string InternalServerError = "مشکلی در سرور به وجود آمده دوباره تلاش کنید!";
		public const string MaxLengthError = "تعداد کاراکتر ها از حد مجاز بیشتر است";
		public const string MinLength = "تعداد کاراکتر ها از حد مجاز کمتر است";

		#endregion

		#region User

		public const string MobileIsInvalid = "شماره همراه نامعتبر است!";
		public const string PasswordLengthError = "  پسورد باید بین 5 تا 8 کاراکتر باشد!";
		public const string PasswordMustContainNumbersAndLetters = "پسورد باید دارای عدد و حروف باشد!";
		public const string DuplicateMobileError = "موبایل وارد شده قبلا ثبت شده است!";
		public const string PasswordConfirmError = "تکرار پسورد اشتباه است!";
		public const string EmailIsInvalid = "این ایمیل قبلا ثبت شده است!";
		public const string DuplicateUserNameError = "این نام کاربری قبلا ثبت شده است!";
		public const string UserNameOrPasswordIsInvalid = "نام کاربری یا پسورد اشتباه است!";
		public const string UserNotFound = "کاربر پیدا نشد!";

		#endregion



	}
}
