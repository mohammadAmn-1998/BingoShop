using Microsoft.AspNetCore.Http;
using Shared.Application;
using System.ComponentModel.DataAnnotations;
using Shared.Application.Utility;

namespace Site.Application.Contract.SiteSettingApplication.Command
{
    public class UbsertSiteSetting
    {
        [Display(Name = "لینک پیج اینستاگرام")] 
        [MaxLength(600, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Instagram { get; set; }
        [Display(Name = "لینک واتس اپ")]
        [MaxLength(600, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? WhatsApp { get; set; }
        [Display(Name = "لینک تلگرام")]
        [MaxLength(600, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Telegram { get; set; }
        [Display(Name = "لینک پیج یوتیوب")]
        [MaxLength(600, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Youtube { get; set; }
        [Display(Name = "لوگو")]
        public IFormFile? LogoFile { get; set; }
        public string? LogoName { get; set; }
        [Display(Name = "Alt لوگو")]
        [MaxLength(155, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? LogoAlt { get; set; }
        [Display(Name = "فاو آیکون")]
        public IFormFile? FavIconFile { get; set; }
        public string? FavIcon { get; set; }
        [Display(Name = "لینک اینماد")]
        public string? Enamad { get; set; }
        [Display(Name = "لینک ساماندهی")]
        public string? SamanDehi { get; set; }
        [Display(Name = "Seo Box")]
        public string? SeoBox { get; set; }
        [Display(Name = "لینک دانلود اپلیکیشن اندروید")]
        [MaxLength(800, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Android { get; set; }
        [Display(Name = "لینک دانلود اپلیکیشن ایفون")]
        [MaxLength(800, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? IOS { get; set; }
        [Display(Name = "عنوان فوتر")]
        [MaxLength(255, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? FooterTitle { get; set; }
        [Display(Name = "توضیحات فوتر")]
        public string? FooterDescription { get; set; }
        [Display(Name = "شماره تماس 1")]
        [MaxLength(11, ErrorMessage = ErrorMessages.MaxLengthError)]
        [MinLength(11, ErrorMessage = ErrorMessages.MinLengthError)]
        public string? Phone1 { get; set; }
        [Display(Name = "شماره تماس 2")]
        [MaxLength(11, ErrorMessage = ErrorMessages.MaxLengthError)]
        [MinLength(11, ErrorMessage = ErrorMessages.MinLengthError)]
        public string? Phone2 { get; set; }
        [Display(Name = "ایمیل 1")]
        [MaxLength(255, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Email1 { get; set; }
        [Display(Name = "ایمیل 2")]
        [MaxLength(255, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Email2 { get; set; }
        [Display(Name = "آدرس")]
        [MaxLength(400, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Address { get; set; }
        [Display(Name = "توضیحات تماس با ما")]
        public string? ContactDescription { get; set; }



        [Display(Name = "توضیحات درباره ما")]
        public string? AboutDescription { get; set; }
        [Display(Name = "عنوان درباره ما")]
        [MaxLength(255, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? AboutTitle { get; set; }
    }
}
