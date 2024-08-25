using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Enums;
using Users1.Application.Contract.UserService.Query;
using Users1.Domain.UserAgg.IRepositories;

namespace Users1.Query.Services
{
	internal class UserQuery : IUserQuery
	{
		private readonly IUserRepository _userRepository;

		public UserQuery(IUserRepository userRepository)
		{
			_userRepository = userRepository;
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
	}
}
