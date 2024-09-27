
using System.ComponentModel.DataAnnotations;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Site.Application.Contract.MenuApplication.Command
{
    public class CreateSubMenu : UbsertMenu
    {
        [Display(Name = "شماره برای ترتیب بندی")]
        public long Number { get; set; } = 0;
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Title { get; set; }
        [Display(Name = "لینک مقصد (برای منو هایی که زیر منو دارند # وارد کنید )")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Url { get; set; }
        public long ParentId { get; set; }
        public MenuStatus ParentStatus { get; set; }
        public string ParentTitle { get; set; }
        public string? ImageName { get; set; }
    }
}
