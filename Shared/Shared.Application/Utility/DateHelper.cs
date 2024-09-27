using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Utility
{
	public static class DateHelper
	{

		private static readonly PersianCalendar PersianCalendar  = new();

		public static string[] MonthNames =
			{"فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"};
		public static string[] DayNames = { "شنبه", "یکشنبه", "دو شنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه" };
		public static string[] DayNamesG = { "یکشنبه", "دو شنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه", "شنبه" };
		private static readonly string[] Pn = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
		private static readonly string[] En = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

		public static string ConvertToPersianDate(this DateTime? time)
		{
			if (time == null)
				return "";

			return time.Value.ConvertToPersianDate();
		}

		public static string ConvertToPersianDate(this DateTime time)
		{

			if (time == new DateTime())
				return "";

			StringBuilder builder = new();
		    builder.Append(PersianCalendar.GetYear(time));
		    builder.Append("/");
		    builder.Append(PersianCalendar.GetMonth(time));
		    builder.Append("/");
		    builder.Append(PersianCalendar.GetDayOfMonth(time));

		    return builder.ToString().ToPersianNumbers();
		}

		private static string ToPersianNumbers(this string stringNumber)
		{
			
			for (var i = 0; i < 10; i++)
				stringNumber  = stringNumber.Replace(En[i], Pn[i]);
			
			return stringNumber;

		}

		private static string ToEnglishNumbers(this string stringNumber)
		{

			for (var i = 0; i < 10; i++)
				stringNumber = stringNumber.Replace(Pn[i], En[i]);

			return stringNumber;

		}

		private static string HowManyDaysPast(DateTime time)
		{

			var yearsLeft = DateTime.Now.Year - time.Year;
			var monthLeft = Math.Abs(DateTime.Now.Month - time.Month);
			var daysPast = Math.Abs(DateTime.Now.DayOfYear - time.DayOfYear);
			var hoursLeft = Math.Abs(DateTime.Now.Hour - time.Hour);
			var minutesLeft = (DateTime.Now.Minute < time.Minute) ? Math.Abs((60 + DateTime.Now.Minute) - time.Minute) : Math.Abs(DateTime.Now.Minute - time.Minute);

			var secondsLeft = Math.Abs(DateTime.Now.Second - time.Second);

			if (yearsLeft != 0)
			{
				return string.Format($"{yearsLeft} سال پیش");
			}

			if (monthLeft != 0 && daysPast > 29)
			{
				return string.Format($"{monthLeft} ماه پیش");
			}

			if (monthLeft == 1 && daysPast <= 29)
			{
				return string.Format($"{daysPast} روز پیش");
			}
			if (daysPast == 0 && hoursLeft != 0)
			{
				if (minutesLeft < 60)
				{
					return string.Format("{0} ساعت{1} و  دقیقه پیش 	", hoursLeft, minutesLeft);
				}

				return string.Format($"{hoursLeft} ساعت پیش");
			}

			if (daysPast == 0 && hoursLeft == 0 && minutesLeft != 0)
			{
				return string.Format($"{minutesLeft} دقیقه پیش");
			}

			if (daysPast == 0 && hoursLeft == 0 && minutesLeft == 0 && secondsLeft != 0)
			{
				return string.Format($"{secondsLeft} ثانیه پیش");
			}

			if (daysPast == 0 && hoursLeft == 0 && minutesLeft == 0 && secondsLeft == 0)
			{
				return string.Format("هم اکنون");
			}


			return string.Format($"{daysPast} روز پیش");

		}



	}
}
