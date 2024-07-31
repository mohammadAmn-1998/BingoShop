
using Shared.Application.Models;
using Shared.Application.Services;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Users.Application.Services.Dtos.UserDtos;
using Users.Application.Services.Interfaces;
using Users.Domain.UserAgg;
using Encoder = Shared.Application.Utility.Encoder;

namespace Users.Application.Services.Implements
{
	internal class UserService : IUserService
	{
		private IUserRepository _userRepository;
		private readonly IFileService _fileService;
		private readonly IAuthService _authService;
		public UserService(IUserRepository userRepository, IFileService fileService, IAuthService authService)
		{
			_userRepository = userRepository;
			_fileService = fileService;
			_authService = authService;
		}


		public OperationResult Create(CreateUserDto dto)
		{

			if (_userRepository.IsExists(x => x.Mobile.Trim() == dto.Mobile.Trim()))
				return new(Status.BadRequest, ErrorMessages.DuplicateMobileError, "Mobile");
			var hashedPass = Encoder.EncodeToSha256(dto.Password);
			var activeKey = RandomGenerator.GenerateRandomUserActiveKey();


			var newUser = User.Register(dto.Mobile,hashedPass,activeKey);

			var success = _userRepository.Insert(newUser);

			if (success)
				return new(Status.Success);

			return new(Status.InternalServerError, ErrorMessages.InternalServerError);

		}

		public OperationResult Login(LoginUserDto dto)
		{
			try
			{
				if (ExistsByUserNameAndPassword(dto.UserName.Trim(), dto.Password))
					return new(Status.BadRequest, ErrorMessages.UserNameOrPasswordIsInvalid);
				var user = _userRepository.GetBy(x => x.UserName == dto.UserName.Trim());

				var isLoggedIn = _authService.Login(new(user.Id, user.UniqueKey, user.Mobile));

				if (!isLoggedIn)
					throw new Exception();

				return new(Status.Success);


			}
			catch (Exception e)
			{
				return new(Status.InternalServerError,ErrorMessages.InternalServerError);
			}
		}

		public OperationResult EditByUser(EditUserByUserDto dto)
		{
			try
			{
				
					var user = _userRepository.GetBy(x => x.Id == dto.Id);

					if (user == null)
						throw new Exception();

					if (user.Mobile.Trim() != dto.Mobile.Trim())
					{
						if (ExistsByMobile(dto.Mobile.Trim()))
							return new(Status.BadRequest, ErrorMessages.DuplicateMobileError);

					}

					if (user.UserName.Trim() != dto.UserName.Trim())
					{
						if (ExistsByUserName(dto.UserName.Trim()))
							return new(Status.BadRequest, ErrorMessages.DuplicateUserNameError);
					}

					var avatarName = "default.png";

					if(dto.AvatarImageFile !=null)
					{
						 avatarName =
							_fileService.UploadFileAndReturnFileName(dto.AvatarImageFile,
								Directories.UserAvatarDirectory);

						if (avatarName is null)
							throw new Exception();

						_fileService.ResizeImage(avatarName, Directories.UserAvatarDirectory100, 100);
						_fileService.ResizeImage(avatarName, Directories.UserAvatarDirectory400, 400);
					}

					user.EditUserByUser(dto.FirstName ?? "",dto.LastName ?? "",dto.UserName ?? "","",avatarName,dto.EmailAddress ?? "",dto.Mobile,dto.Biography ?? "",dto.Gender);

					if (!_userRepository.Update(user))
					{
						_fileService.DeleteFile(avatarName, Directories.UserAvatarDirectory);
						_fileService.DeleteFile(avatarName, Directories.UserAvatarDirectory100);
						_fileService.DeleteFile(avatarName, Directories.UserAvatarDirectory400);

						throw new Exception();
					}
				
					return new(Status.Success);

			}
			catch (Exception e)
			{
				return new(Status.InternalServerError,ErrorMessages.InternalServerError);
			}
		}

		public EditUserByUserDto GetForEditByUser(int userId)
		{
			try
			{
				var user = _userRepository.GetBy(x => x.Id == userId);
				if (user == null)
					throw new Exception();

				return new()
				{
					Id = user.Id,
					FirstName = user.FirstName,
					LastName = user.LastName,
					UserName = user.UserName,
					AvatarImageFile = null,
					avatarImagaName = user.Avatar,
					EmailAddress = user.EmailAddress,
					Mobile = user.Mobile,
					Biography = user.Biography,
					Gender = user.Gender
				};

			}
			catch (Exception e)
			{
				return new();
			}
		}

		public EditUserByAdminDto GetForEditByAdmin(int userId)
		{
			try
			{
				var user = _userRepository.GetBy(x => x.Id == userId);
				if (user == null)
					throw new Exception();

				return new()
				{
					Id = user.Id,
					FirstName = user.FirstName,
					LastName = user.LastName,
					UserName = user.UserName,
					AvatarImageFile = null,
					avatarImagaName = user.Avatar,
					EmailAddress = user.EmailAddress,
					Mobile = user.Mobile,
					Biography = user.Biography,
					Gender = user.Gender
				};

			}
			catch (Exception e)
			{
				return new();
			}
		}

		public bool ExistsByUserName(string username)
		{
			try
			{
			  return _userRepository.IsExists(x => x.UserName.Trim() == username.Trim());
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public bool ExistsByEmail(string email)
		{

			try
			{
				return _userRepository.IsExists(x => x.EmailAddress.Trim() == email.Trim());
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public bool ExistsByMobile(string mobile)
		{

			try
			{
				return _userRepository.IsExists(x => x.Mobile.Trim() == mobile.Trim());
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public bool ExistsByUserNameAndPassword(string userName, string password)
		{
			try
			{
				var hashedPassword = Encoder.EncodeToSha256(password.Trim());
				return _userRepository.IsExists(x =>
					x.UserName == userName.Trim() && x.Password.Trim() == hashedPassword);
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public void ChangeActivation(int userId)
		{
			var user = _userRepository.GetBy(x => x.Id == userId);

			if (user is not null)
			{
				user.CreateActiveKey();
				_userRepository.Save();
			}
		}

		public void ChangeBlock(int userId)
		{
			var user = _userRepository.GetBy(x => x.Id == userId);

			if (user is not null)
			{
				user.ChangeBlock();
				_userRepository.Save();
			}
		}
	}
}
