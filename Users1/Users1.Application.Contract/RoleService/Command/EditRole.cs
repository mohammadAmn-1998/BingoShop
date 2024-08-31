using System.ComponentModel.DataAnnotations;

namespace Users1.Application.Contract.RoleService.Command;

public class EditRole : CreateRole
{
    public long Id { get; set; }

    [Display(Name = "دسترسی ها")]
	public List<EditPermission>? EditPermissions { get; set; }
}