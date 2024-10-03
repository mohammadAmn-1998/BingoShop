using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Utility
{
	public  static class Directories
	{

		#region Article


		public const string ArticleImageDirectory = "wwwroot/assets/images/article_img";
		public const string ArticleImageDirectory100 = "wwwroot/assets/images/article_img100";
		public const string ArticleImageDirectory400 = "wwwroot/assets/images/article_img400";

		#endregion

		#region BlogCategory

		public const string BlogCategoryImageDirectory = "wwwroot/assets/images/blog_category_img";
		public const string BlogCategoryImageDirectory400 = "wwwroot/assets/images/blog_category_img400";
		public const string BlogCategoryImageDirectory100 = "wwwroot/assets/images/blog_category_img100";

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


		#endregion

		#region User

		public const string UserAvatarDirectory = "wwwroot/assets/images/user_img";
		public const string UserAvatarDirectory100 = "wwwroot/assets/images/user_img100";
		public const string UserAvatarDirectory400 = "wwwroot/assets/images/user_img400";

		public static string GetUserAvatarFullPath(string? imageName, int? imageSize)
		{

			if (imageName is "Default.png" or " " or null)
				return UserAvatarDirectory.Replace("wwwroot", "") + "/" + "Default.png";

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


		#endregion

		#region Blog

		public const string BlogImageDirectory = "wwwroot/assets/images/blog_img";
		public const string BlogImageDirectory100 = "wwwroot/assets/images/blog_img";
		public const string BlogImageDirectory400 = "wwwroot/assets/images/blog_img";
		public const string BlogContentImageDirectory = "wwwroot/assets/images/blog_content_img";

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

			return BlogContentImageDirectory.Replace("wwwroot", "") + "/" + imageName;

		}

		#endregion

		#region Baner

		public const string BanerImageDirectory = "wwwroot/assets/images/baner_img";
		public const string BanerImageDirectory100 = "wwwroot/assets/images/baner_img100";
		public const string BanerImageDirectory400 = "wwwroot/assets/images/baner_img400";

		public static string GetBanerImageFullPath(string? imageName, int? size)
		{
			if (string.IsNullOrEmpty(imageName))
				return ImageSiteDirectory.Replace("wwwroot", "") + "/" + "Default.jpg";

			switch (size)
			{
				case 100:
					return BanerImageDirectory100.Replace("wwwroot", "") + "/" + imageName;

				case 400:
					return BanerImageDirectory400.Replace("wwwroot", "") + "/" + imageName;

				default:
					return BanerImageDirectory.Replace("wwwroot", "") + "/" + imageName;
			}

		}

		#endregion

		#region ImageSite

		public const string ImageSiteDirectory = "wwwroot/assets/images/image_site_img";
		public const string ImageSiteDirectory100 = "wwwroot/assets/images/image_site_img100";
		public const string ImageSiteDirectory400 = "wwwroot/assets/images/image_site_img400";


		#endregion

		#region ManuImage

		public const string MenuImageDirectory = "wwwroot/assets/images/menu_img";
		public const string MenuImageDirectory100 = "wwwroot/assets/images/menu_img100";
		public const string MenuImageDirectory400 = "wwwroot/assets/images/menu_img400";

		#endregion

		#region ServiceImage

		public const string ServiceImageDirectory = "wwwroot/assets/images/service_img";
		public const string ServiceImageDirectory100 = "wwwroot/assets/images/service_img100";
		public const string ServiceImageDirectory400 = "wwwroot/assets/images/service_img400";

		#endregion

		#region SiteSetting


		public const string SiteSettingImageDirectory = "wwwroot/assets/images/site_setting_img";
		public const string SiteSettingImageDirectory16 = "wwwroot/assets/images/site_setting_img16";
		public const string SiteSettingImageDirectory32 = "wwwroot/assets/images/site_setting_img32";
		public const string SiteSettingImageDirectory64 = "wwwroot/assets/images/site_setting_img64";
		public const string SiteSettingImageDirectory100 = "wwwroot/assets/images/site_setting_img100";
		public const string SiteSettingImageDirectory300 = "wwwroot/assets/images/site_setting_img300";
		public const string SiteSettingImageDirectory400 = "wwwroot/assets/images/site_setting_img400";


		#endregion

		#region Slider

		public const string SliderImageDirectory = "wwwroot/assets/images/slider_img";
		public const string SliderImageDirectory100 = "wwwroot/assets/images/slider_img100";

		#endregion

		#region PackageImage

		public const string PackageImageDirectory = "wwwroot/assets/images/package_img";
		public const string PackageImageDirectory100 = "wwwroot/assets/images/package_img100";
		public const string PackageImageDirectory400 = "wwwroot/assets/images/package_img400";

		#endregion


	}
}
