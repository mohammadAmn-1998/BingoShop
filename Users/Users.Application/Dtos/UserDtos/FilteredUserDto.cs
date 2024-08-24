using Shared.Application.Models;
using Shared.Application.Utility;

namespace Users.Application.Dtos.UserDtos;

public class FilteredUserDto : BasePagination
{

	public List<UserDto>? FilteredUsers { get; set; }

	public FilterParams FilterParams { get; set; }

}