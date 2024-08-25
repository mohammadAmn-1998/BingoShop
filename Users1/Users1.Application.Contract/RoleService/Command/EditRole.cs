namespace Users1.Application.Contract.RoleService.Command;

public class EditRole : CreateRole
{
    public long Id { get; set; }

    public List<EditPermission> Permissions { get; set; }
}