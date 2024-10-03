using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Utility
{
	public static class TextHelper
	{


		public static string ToShortText(this string text)
		{

			if (text.Length > 20)
			{

				return text[..20] + " ...";
			}

			return text;

		}

	}
}
