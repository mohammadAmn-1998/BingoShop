using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Models;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;
using Users1.Application.Contract.RoleService.Query;
using Users1.Application.Contract.UserService.Query;
using Users1.Domain.UserAgg;
using Users1.Domain.UserAgg.IRepositories;
using Users1.Infrastructure.Context;

namespace Users1.Query.Services
{
	internal class UserQuery : BaseRepository , IUserQuery
	{
		private readonly IUserRepository _userRepository;
		private readonly UsersContext _usersContext;

		public UserQuery(IUserRepository userRepository , UsersContext usersContext ) : base (usersContext)
		{
			_userRepository = userRepository;
			_usersContext = usersContext;
		}


		public List<UserQueryModel>? GetAll()
		{

			try
			{
				var users = Table<User>().Include(x => x.UserRoles).ThenInclude(x => x.Role)
					.ThenInclude(x => x.Permissions).OrderByDescending(x => x.CreateDate).Select(x => new UserQueryModel
					{
						userId = x.Id,
						UserName = x.UserName,
						FullName = x.FullName,
						Avatar = x.Avatar,
						UserUniqueCode = x.UserUniqueCode,
						biography = x.Biography,
						Gender = x.Gender,
						Mobile = x.Mobile,
						Email = x.Email,
						IsActive = x.Active,
						Roles = x.UserRoles.Select(x=> new RoleQueryModel()
						{
							Id = x.RoleId,
							CreateDate = x.Role.CreateDate,
							Title = x.Role.Title,
							UpdateDate = x.Role.UpdateDate,
							Permissions = new(),
							Users = new()
							
						}).ToList()
					}).ToList();

				return users;

			}
			catch (Exception e)
			{
				return null;
			}
		}

		public UserQueryModel? GetUserBy(long userId)
		{
			try
			{
				var user = _userRepository.GetById(userId);
				if (user == null) throw new NullReferenceException();

				return new()
				{
					userId = user.Id,
					UserName = user.UserName,
					FullName = user.FullName,
					biography = user.Biography,
					Gender = user.Gender,
					Mobile = user.Mobile,
					Email = user.Email,
					Avatar = user.Avatar,
					UserUniqueCode = user.UserUniqueCode,
				};
			}
			catch (Exception e)
			{
				return null;
			}
		}

		public UserQueryModel? GetUserByMobile(string mobile)
		{
			try
			{
				var user = _userRepository.GetByMobile(mobile.Trim());
				if (user == null) throw new NullReferenceException();

				return new()
				{
					userId = user.Id,
					UserName = user.UserName,
					FullName = user.FullName,
					biography = user.Biography,
					Gender = user.Gender,
					Mobile = user.Mobile,
					Email = user.Email,
					Avatar = user.Avatar,
					UserUniqueCode = user.UserUniqueCode,
				};
			}
			catch (Exception e)
			{
				return null;
			}
		}

		public FilteredUsersQueryModel GetFilteredUsers(FilterParams filterParams)
		{
			try
			{

				var result = Table<User>().Include(x => x.UserRoles).ThenInclude(x => x.Role).AsQueryable();

				if (!string.IsNullOrEmpty(filterParams.Title))
				{
					result = result.Where(x => (x.UserName.Contains(filterParams.Title)) || (!x.UserName.Contains(filterParams.Title )&& x.Mobile.Contains(filterParams.Title)));
				}

				var skip = (filterParams.PageId - 1) * filterParams.Take;

				FilteredUsersQueryModel filteredUsers = new();

				filteredUsers.GetBasePagination(result,filterParams.PageId,filterParams.Take);

				result = result.Skip(skip).Take(filterParams.Take);

				filteredUsers.FilterParams = filterParams;
				filteredUsers.Users = result.Select(x => new UserQueryModel
				{
					userId = x.Id,
					UserName = x.UserName,
					FullName = x.FullName,
					Avatar = x.Avatar,
					UserUniqueCode = x.UserUniqueCode,
					biography = x.Biography,
					Gender = x.Gender,
					Mobile = x.Mobile,
					Email = x.Email,
					Roles = x.UserRoles.Select(x => new RoleQueryModel()
					{
						Id = x.RoleId,
						CreateDate = x.Role.CreateDate,
						Title = x.Role.Title,
						UpdateDate = x.Role.UpdateDate,
						Permissions = new(),
						Users = new()

					}).ToList(),
					IsActive = x.Active,
					
				}).ToList();

				return filteredUsers;
			}
			catch (Exception e)
			{
				return new();
			}
		}

		public List<UserPermission> GetUserPermissionsById(long userId)
		{
			try
			{
				List<UserPermission> userPermissions = new();
				var userRoles = Table<UserRole>().Include(x => x.Role).ThenInclude(x => x.Permissions)
					.Where(x => x.UserId == userId);

				if (userRoles.Any())
				{
					userPermissions = userRoles.SelectMany(x => x.Role.Permissions.Select(p => p.UserPermission)).ToList();
				}

				return userPermissions;

			}
			catch (Exception e)
			{
				return new();
			}
		}

		public List<string> GetUserRolesTitleBy(long userId)
		{
			try
			{
				List<string> roles = new();
				var userRoles = Table<UserRole>().Include(x => x.Role)
					.Where(x => x.UserId == userId);

				if (userRoles.Any())
				{
					roles = userRoles.Select(x=> x.Role.Title).ToList();
				}

				return roles;

			}
			catch (Exception e)
			{
				return new();
			}
		}
	}
}
