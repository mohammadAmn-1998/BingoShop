using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Users.Application.Dtos.PermissionDtos;
using Users.Application.Dtos.RoleDtos;
using Users.Application.Dtos.UserDtos;
using Users.Application.Dtos.UserRoleDtos;
using Users.Application.Services.Interfaces;
using Users.Domain.UserAgg;

namespace Users.Application.Services.Implements
{
	internal class RoleService : IRoleService
	{

		private readonly  IRoleRepository _roleRepository;
		private readonly IUserRoleRepository _userRoleRepository;

		public RoleService(IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
		{
			_userRoleRepository = userRoleRepository;
			_roleRepository = roleRepository;
		}

		public List<RoleDto>? GetRoles()
		{
			try
			{
				var roles = _roleRepository.GetAll(eager: true)?.Select(x => new RoleDto
				{
					RoleId = x.Id,
					Title = x.Title,
					PermissionDtos = x.Permissions.Select(x => new PermissionDto
					{
						Id = x.Id,
						RoleId = x.RoleId,
						UserPermission = x.UserPermission
					}).ToList()
					,
					UserRoleDtos = x.UserRoles.Select(x=> new UserRoleDto
					{
						Id = x.Id,
						RoleId = x.RoleId,
						UserId = x.UserId,
						User = new UserDto()
						{
							Id = x.User.Id,
							UserName = x.User.UserName,
							Avatar = x.User.Avatar,
							Email = x.User.EmailAddress,
							IsActive = x.User.Active,
						},
						Role = new RoleDto
						{
							RoleId = x.RoleId,
							Title = x.Role.Title,
							PermissionDtos =new() ,
							UserRoleDtos = new(),
						}
					}).ToList()
				}).ToList();

				return roles;
			}
			catch (Exception e)
			{
				return null;
			}
		}

		public OperationResult CreateRole(CreateRoleDto dto, List<UserPermission> permissions)
		{
			return _roleRepository.Create(new Role(dto.Title), permissions);
		}

		public OperationResult DeleteRole(int roleId)
		{

			return _roleRepository.Delete(roleId);
		}

		public OperationResult EditRole(EditRoleDto dto, List<UserPermission> permissions)
		{
			var role = _roleRepository.GetBy(x=> x.Id == dto.RoleId);

			if (role == null)
				return new(Status.NotFound,ErrorMessages.RoleNotFound);

			return _roleRepository.Edit(role, permissions);
		}

		public bool CheckPermission(int userId, UserPermission permission)
		{
			try
			{

				var userRoles = _userRoleRepository.GetUserRoles(userId);
				if(userRoles == null)
					return false;

				foreach (var userRole in userRoles)
				{
					if(userRole.Role.Permissions.Any(x=> x.UserPermission == permission))
						return true;

				}

				return false;
			}
			catch (Exception e)
			{

				return false;
			}
		}
	}
}
