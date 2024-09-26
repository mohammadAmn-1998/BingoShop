
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;

namespace Users1.Domain.UserAgg
{
	public class User : BaseEntityUpdateActive<long>
	{

		public string FullName { get;private set; }

		public string UserName { get; private set; }

		public string UserUniqueCode { get; private set; }

		public string Avatar { get; private set; }

		public string PassKey { get; private set; }

		public string ActiveKey { get; private set; }

		public Gender Gender { get; private set; } 

		public string Email { get; private set; }

		public string Mobile { get; private set; }

		public string? Biography { get; private set; }

		public bool IsBanned  { get; private set; }

		public List<UserRole> UserRoles { get; set; }

		

		public User()
		{
			UserRoles = new();
		}

		public User(string fullName, string userName, string userUniqueCode, string avatar , string passKey, string activeKey, Gender gender, string email, string mobile, string? biography)
		{
			FullName = fullName;
			UserName = userName;
			UserUniqueCode = userUniqueCode;
			Avatar = avatar;
			PassKey = passKey;
			ActiveKey = activeKey;
			Gender = gender;
			Email = email;
			Mobile = mobile;
			Biography = biography;
			
			
		}

		public void Edit(string fullName, string avatar, Gender gender,string? biography)
		{
			FullName = fullName;
			Avatar = avatar;
			Gender = gender;
			UpdateDate = DateTime.Now;
			Biography = biography;
		}

		public void EditByAdmin(string fullName, string mobile,
			string email, string avatar, Gender gender, string? biography )
		{
			FullName = fullName;
			Mobile = mobile;
			Email = email;
			Avatar = avatar;
			Gender = gender;
			Biography = biography;
			UpdateDate = DateTime.Now;

		}

		public static User Register(string mobile,string passKey)
		{
			return new("", mobile, Guid.NewGuid().ToString(), "default.png",passKey , " ",Gender.نامشخص," ",mobile,null);
		}

		public void ChangePassKey(string passKey)
		{
			PassKey = passKey;
		}

		public void ChangeBane()
		{
			IsBanned = !IsBanned;
		}

	}
}
