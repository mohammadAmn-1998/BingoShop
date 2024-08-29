using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;

namespace Users.Domain.UserAgg
{
	public class User : BaseEntity<int>
	{

		public string FirstName { get; private set; }

		public string LastName { get; private set; }

		public string UserName { get; private set; }

		public string Password { get; private set; }

		public string Avatar { get; private set; }

		public string EmailAddress { get; private set; }

		public string Mobile { get; private set; }

		public string Biography { get; private set; }

		public DateTime? UpdateDate { get; private set; }

		public string ActiveKey { get; private set; }

		public Gender Gender { get; private set; }

		public string UniqueKey { get; private set; }

		public bool Block { get; private set; }

		public List<UserAddress> UserAddresses { get; private set; }
		public List<UserRole> UserRoles { get; private set; }

		public User()
		{
			UserAddresses = new();
			UserRoles = new();
		}

		public User(string firstName, string lastName, string password, string avatar, string emailAddress, string mobile, string biography, string activeKey, Gender gender)
		{
			FirstName = firstName;
			LastName = lastName;
			if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
			{
				UserName = firstName + " " + lastName;
			}
			else
			{
				UserName = mobile;
			}

			Password = password;
			Avatar = avatar;
			EmailAddress = emailAddress;
			Mobile = mobile;
			Biography = biography;
			Gender = gender;
			UniqueKey = Guid.NewGuid().ToString();
			CreateDate = DateTime.Now;
			
		}

		public static User Register(string mobile,string password,string activeKey)
		{
			return new User("", "", password, "default.png","",mobile,"", activeKey, Shared.Domain.Enums.Gender.نامشخص)
				;
		}

		public void EditUserByAdmin(string firstName, string lastName, string userName,string password, string avatar,
			string emailAddress, string mobile, string biography, string activeKey, Gender gender )
		{
			FirstName = firstName;
			LastName = lastName;
			Password = password;
			Avatar = avatar;
			EmailAddress = emailAddress;
			Mobile = mobile;
			UserName = userName;
			Biography = biography;
			Gender = gender;
			UpdateDate = DateTime.Now;
		}

		public void EditUserByUser(string firstName, string lastName,string userName, string password, string avatar,
			string emailAddress, string mobile, string biography, Gender gender)
		{
			FirstName = firstName;
			LastName = lastName;
			UserName = userName;
			Password = password;
			Avatar = avatar;
			EmailAddress = emailAddress;
			Mobile = mobile;
			Biography = biography;
			Gender = gender;
			
			UpdateDate = DateTime.Now;
		}

		public void ChangePassword(string newPassword)
		{
			UpdateDate = DateTime.Now;
			Password = newPassword;
		}

		public void ActivationChange()
		{
			Active = !Active;
		}

		public void ChangeBlock()
		{
			Block = !Block;
		}

		public void CreateActiveKey()
		{
			ActiveKey = Guid.NewGuid().ToString();
		}

	}
}
