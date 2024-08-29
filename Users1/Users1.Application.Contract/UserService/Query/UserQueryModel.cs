using Shared.Domain.Enums;
using Users1.Application.Contract.RoleService.Query;

namespace Users1.Application.Contract.UserService.Query;

public class UserQueryModel
{

	public long userId { get; set; }

	public string UserName { get; set; }

	public string FullName { get; set; }

	public string Avatar { get; set; }

	public string UserUniqueCode { get; set; }

	public string? biography { get; set; }

	public Gender Gender { get; set; }

	public string Mobile { get; set; }

	public string Email { get; set; }

	public List<RoleQueryModel> Roles { get; set; }

	public bool IsActive { get; set; }





}