using Query.Contract.Ui.Seo;
using System.ComponentModel.DataAnnotations;
using Query.Contract.Ui.Blog;

namespace Site.Application.Contract.SiteSettingApplication.Query
{
	public class ContactUIQueryModel
	{

		public ContactUIQueryModel()
		{
			
		}
		public ContactUIQueryModel(string? address, string? phone1, string? phone2, string? email1, string? email2)
		{
			Address = address;
			Phone1 = phone1;
			Phone2 = phone2;
			Email1 = email1;
			Email2 = email2;
		}

		public SeoUIQueryModel Seo { get; set; }

		public List<BreadCrumbUiQueryModel> BreadCrumbs { get; set; }

		[Display(Name = "آدرس : ")]
		public string? Address { get; set; }

		[Display(Name = "شماره تلفن اول : ")]
		public string? Phone1{ get; set; }

		[Display(Name = "شماره تلفن دوم : ")]
		public string? Phone2 { get; set; }

		[Display(Name = "آدرس ایمیل اول : ")]
		public string? Email1 { get; set; }

		[Display(Name = "آدرس ایمیل دوم : ")]
		public string? Email2 { get; set; }
		

	}
}