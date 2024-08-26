using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;
using Users1.Application.Contract.UserService.Command;
using Users1.Domain.UserAgg;
using Users1.Domain.UserAgg.IRepositories;
using Users1.Infrastructure.Context;

namespace Users1.Infrastructure.Repositories
{
	internal class UserRepository : BaseRepository , IUserRepository
	{
		public UserRepository(UsersContext context) : base(context)
		{
		}


		public async Task<bool> CheckPermission(long userId, UserPermission permission)
		{


			try
			{
				var userRoles = await Table<UserRole>().Include(x => x.Role).ThenInclude(x => x.Permissions).Where(x => x.UserId == userId).ToListAsync();

				return userRoles.SelectMany(userRole => userRole.Role.Permissions).Any(userPermission => userPermission.UserPermission == permission);

			}
			catch (Exception e)
			{
				return false;
			}

		}

		public User? GetByMobile(string mobile)
		{
			try
			{
			
				return Table<User>().SingleOrDefault(x => x.Mobile == mobile.Trim());
			}
			catch (Exception e)
			{
				return null;
			}
		}

		public User? GetById(long id)
		{
			try
			{
				return Table<User>().Include(x=> x.UserRoles).SingleOrDefault(x => x.Id == id);
			}
			catch (Exception e)
			{
				return null;
			}
		}

		public async Task<bool> ChangePassKey(string mobile,string passKey)
		{
			try
			{
				var user = GetByMobile(mobile.Trim());
				if (user == null) return false;

				user.ChangePassKey(passKey);
				Update(user);
				return await Save() > 0;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public async Task<bool> ChangeActivation(long userId)
		{
			try
			{
				var user = GetById(userId);
				if(user == null) return false;

				user.ActivationChange();
				Update(user);

				return await Save() > 0;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public async Task<bool> ChangeBan(long userId)
		{
			try
			{
				var user = GetById(userId);
				if (user == null) return false;

				user.ChangeBane();
				Update(user);

				return await Save() > 0;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public async Task<bool> Create(User user)
		{
			try
			{

				Insert(user);
				return await Save() > 0;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public async Task<bool> EditByAdmin(EditUserByAdmin command)
		{
			try
			{

				var user = GetById(command.Id);
				if(user is null) return false;

				user.EditByAdmin(command.FullName ?? "" ,command.Mobile,command.Email?? "",command.AvatarName,command.UserGender,command.biography);

				return await Save() > 0;

			}
			catch (Exception e)
			{
				return false;
			}
		}

		public async Task<bool> EditByUser(EditUserByUser command)
		{
			try
			{

				var user = GetById(command.Id);
				if (user is null) return false;

				user.Edit(command.FullName ?? "", command.AvatarName, command.UserGender, command.biography);

				return await Save() > 0;

			}
			catch (Exception e)
			{
				return false;
			}
		}
	}
}
