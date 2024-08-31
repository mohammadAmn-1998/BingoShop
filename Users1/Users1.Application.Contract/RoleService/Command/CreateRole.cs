using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Users1.Application.Contract.RoleService.Command
{
    public class CreateRole
    {

        [Display(Name = "عنوان نقش")]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public string Title { get; set; }

        [Display(Name = "دسترسی ها")]
        public List<UserPermission>? Permissions { get; set; }
    }
}
