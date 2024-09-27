
using System.ComponentModel.DataAnnotations;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Site.Application.Contract.MenuApplication.Command
{
    public class CreateMenu : UbsertMenu
    {
        [Display(Name = "نوع منو")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public MenuStatus Status { get; set; }

        public string? ImageName { get; set; }
    }
}
