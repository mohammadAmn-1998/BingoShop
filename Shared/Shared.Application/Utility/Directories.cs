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

		public const string BlogImageDirectory = "wwwroot/assets/images/blog_img";
		public const string BlogImageDirectory100 = "wwwroot/assets/images/blog_img";
		public const string BlogImageDirectory400 = "wwwroot/assets/images/blog_img";

		public const string BlogContentImageDirectory = "wwwroot/assets/images/blog_content_img";

		public static string GetUserAvatarFullPath(string? imageName,int? imageSize)
		{

			if (imageName is "Default.png" or " " or null)
				return UserAvatarDirectory.Replace("wwwroot","") + "/" + "Default.png";

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

		public static string GetBlogCategoryImageFullPath(string? imageName, int? imageSize)
		{

			if (imageName is "Default.png" or " " or null)
				return BlogCategoryImageDirectory.Replace("wwwroot", "") + "/" + "Default.png";

			switch (imageSize)
			{
				case null:
					return BlogCategoryImageDirectory.Replace("wwwroot", "") + "/" + imageName;
				case 100:
					return BlogCategoryImageDirectory100.Replace("wwwroot", "") + "/" + imageName;
				case 400:
					return BlogCategoryImageDirectory400.Replace("wwwroot", "") + "/" + imageName;
			}

			return BlogCategoryImageDirectory.Replace("wwwroot", "") + "/" + "Default.png";
		}

		public static string GetBlogImageFullPath(string? imageName, int? imageSize)
		{

			if (imageName is "Default.jpg" or " " or null)
				return BlogImageDirectory.Replace("wwwroot", "") + "/" + "Default.jpg";

			switch (imageSize)
			{
				case null:
					return BlogImageDirectory.Replace("wwwroot", "") + "/" + imageName;
				case 100:
					return BlogImageDirectory100.Replace("wwwroot", "") + "/" + imageName;
				case 400:
					return BlogImageDirectory400.Replace("wwwroot", "") + "/" + imageName;
			}

			return BlogImageDirectory.Replace("wwwroot", "") + "/" + "Default.jpg";
		}

		public static string GetBlogContentImageFullPath(string imageName)
		{

			return BlogContentImageDirectory.Replace("wwwroot","") + "/"+ imageName;

		}

	}
}
