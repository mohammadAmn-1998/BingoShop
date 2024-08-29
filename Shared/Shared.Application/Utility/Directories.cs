using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Utility
{
	public  static class Directories
	{

		public const string ArticleImageDirectory = "wwwroot/assets/images/article_img";
		public const string ArticleImageDirectory100 = "wwwroot/assets/images/article_img100";
		public const string ArticleImageDirectory400 = "wwwroot/assets/images/article_img400";
		public const string BlogCategoryImageDirectory = "wwwroot/assets/images/blog_category_img";
		public const string BlogCategoryImageDirectory400 = "wwwroot/assets/images/blog_category_img400";
		public const string BlogCategoryImageDirectory100 = "wwwroot/assets/images/blog_category_img100";
		public const string UserAvatarDirectory = "wwwroot/assets/images/user_img";
		public const string UserAvatarDirectory100 = "wwwroot/assets/images/user_img100";
		public const string UserAvatarDirectory400 = "wwwroot/assets/images/user_img400";


		public static string GetUserAvatarFullPath(string imageName,int? imageSize)
		{

			if (imageName is "Default.png" or " ")
				return UserAvatarDirectory.Replace("wwwroot","") + "/" + imageName;

			switch (imageSize)
			{
				case null:
					return UserAvatarDirectory.Replace("wwwroot", "") + "/" + imageName;
				case 100:
					return UserAvatarDirectory100.Replace("wwwroot", "") + "/" + imageName;
				case 400:
					return UserAvatarDirectory400.Replace("wwwroot", "") + "/" + imageName;
			}

			return UserAvatarDirectory.Replace("wwwroot", "") + "/" + "Default.png";
		}


	}
}
