using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Users1.Application.Contract.RoleService.Command;
using Users1.Domain.UserAgg;
using Users1.Domain.UserAgg.IRepositories;

namespace Users1.Application.Services
{
    internal class RoleService : IRoleService
	{

		private readonly IRoleRepository _roleRepository;
		private readonly IUserRepository _userRepository;
		public RoleService(IRoleRepository roleRepository, IUserRepository userRepository)
		{
			_roleRepository = roleRepository;
			_userRepository = userRepository;
		}

		public async Task<bool> CheckPermission(long userId, UserPermission permission)
		{
			return await _userRepository.CheckPermission(userId, permission);
		}

		public async Task<OperationResult> CreateRole(CreateRole command)
		{
			try
			{

				if (_roleRepository.ExistBy(command.Title.Trim()))
					return new(Status.BadRequest, ErrorMessages.DuplicateError);

				if (await _roleRepository.Create(command))
					return new(Status.Success);

				throw new Exception();


			}
			catch (Exception e)
			{
				return new(Status.InternalServerError, ErrorMessages.InternalServerError);
			}
		}

		public async Task<OperationResult> EditRole(EditRole command)
		{
			try
			{
				var role = _roleRepository.GetById(command.Id);
				if (role == null) throw new NullReferenceException();

				if (role.Title.Trim() != command.Title.Trim())
				{
					if (_roleRepository.ExistBy(command.Title.Trim()))
						return new(Status.BadRequest, ErrorMessages.DuplicateError);
				}
				

				if (await _roleRepository.Edit(command))
					return new(Status.Success);

				throw new Exception();


			}
			catch (Exception e)
			{
				return new(Status.InternalServerError, ErrorMessages.InternalServerError);
			}
		}

		public EditRole GetRoleForEdit(long roleId)
		{
			try
			{
				var role = _roleRepository.GetById(roleId);
				if (role is null) throw new NullReferenceException();

				return new()
				{
					Title = role.Title,
					Id = role.Id,
					Permissions = role.Permissions.Select(x=> new EditPermission()
					{
						UserPermission = x.UserPermission
					}).ToList()
				};
			}
			catch (Exception e)
			{
				return new();
			}
		}


		public async Task<OperationResult> EditUserRole(long userId, List<long> roles)
		{
			try
			{
				var user = _userRepository.GetById(userId);
				if(user is null) throw new NullReferenceException();

				//delete old userRoles
				var oldUserRoles = user.UserRoles;
				if (!await _roleRepository.DeleteUserRoles(oldUserRoles))
					throw new Exception();

				var ok = await _roleRepository.AddRolesToUser(userId, roles);
				if(!ok) throw new Exception();

				return new(Status.Success);
			}
			catch (Exception e)
			{
				return new(Status.InternalServerError, ErrorMessages.InternalServerError);
			}
		}
	}
}
