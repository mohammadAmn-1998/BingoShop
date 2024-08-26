﻿using Shared.Application.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Services;
using Shared.Domain.Enums;
using Users1.Application.Contract.UserService.Command;
using Users1.Domain.UserAgg;
using Users1.Domain.UserAgg.IRepositories;

namespace Users1.Application.Services
{
	internal class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IAuthService _authService;
		private readonly IFileService _fileService;

		public UserService(IUserRepository userRepository, IAuthService authService, IFileService fileService)
		{
			_userRepository = userRepository;
			_authService = authService;
			_fileService = fileService;
		}

		public async Task<bool> Register(RegisterUser command)
		{
			try
			{
				var user = _userRepository.GetByMobile(command.Mobile.Trim());
				var passkey = RandomGenerator.GenerateRandomUserTwoStepVerificationPassKey();

				if (user == null)
				{
					
					user = User.Register(command.Mobile, passkey);
					if(await _userRepository.Create(user))
					{
						//send sms passkey code
						return true;
					}
					return false;

				}
				else
				{
					if( await _userRepository.ChangePassKey(command.Mobile, passkey))
					//send sms passkey code
					return true;

					return false;
				}
			}
			catch (Exception e)
			{
				return false;
			}

		}

		public OperationResult Login(LoginUser command)
		{
			try
			{
				var user = _userRepository.GetByMobile(command.Mobile);
				if (user == null) return new(Status.NotFound, ErrorMessages.MobileNotFound);

				if (user.PassKey != command.PassKey.Trim())
				{
					return new(Status.BadRequest, ErrorMessages.PasskeyIsInvalid,nameof(command.PassKey));
				}
					

				var ok = _authService.Login(new(user.Id, user.UserUniqueCode, user.Mobile));
				if (!ok)
				{
					throw new Exception();
				}

				return new(Status.Success);
			}
			catch (Exception e)
			{
				return new(Status.InternalServerError, ErrorMessages.InternalServerError, "");
			}
		}

		// public OperationResult Create(CreateUser command)
		// {
		// 	throw new NotImplementedException();
		// }

		public async Task<OperationResult> Edit(EditUserByAdmin command)
		{
			try
			{
				var user = _userRepository.GetById(command.Id);
				if (user == null) return new(Status.NotFound, ErrorMessages.UserNotFound);

				if (command.AvatarFile is not null)
				{
					if (!command.AvatarFile.IsImage())
						return new(Status.BadRequest, ErrorMessages.IsNotImage, "AvatarFile");

					var imageName = _fileService.UploadFileAndReturnFileName(command.AvatarFile, Directories.UserAvatarDirectory);
					if (imageName is null) throw new NullReferenceException();

					_fileService.ResizeImage(imageName, Directories.UserAvatarDirectory100, 100);
					_fileService.ResizeImage(imageName, Directories.UserAvatarDirectory400, 400);

					command.AvatarName = imageName;
					if (await _userRepository.EditByAdmin(command))
						return new(Status.Success);

					//else must delete new uploaded images;

					_fileService.DeleteFile(command.AvatarName, Directories.UserAvatarDirectory);
					_fileService.DeleteFile(command.AvatarName, Directories.UserAvatarDirectory100);
					_fileService.DeleteFile(command.AvatarName, Directories.UserAvatarDirectory400);

					throw new Exception();
				}

				if (await _userRepository.EditByAdmin(command))
					return new(Status.Success);

				throw new Exception();


			}
			catch (Exception e)
			{
				return new(Status.InternalServerError, ErrorMessages.InternalServerError, "");
			}
		}

		public async Task<OperationResult> EditByUser(EditUserByUser command, int userId)
		{
			try
			{
				var user = _userRepository.GetById(command.Id);
				if (user == null) return new(Status.NotFound, ErrorMessages.UserNotFound);

				if (command.AvatarFile is not null)
				{
					if (!command.AvatarFile.IsImage())
						return new(Status.BadRequest, ErrorMessages.IsNotImage, "AvatarFile");

					var imageName = _fileService.UploadFileAndReturnFileName(command.AvatarFile, Directories.UserAvatarDirectory);
					if (imageName is null) throw new NullReferenceException();

					_fileService.ResizeImage(imageName, Directories.UserAvatarDirectory100, 100);
					_fileService.ResizeImage(imageName, Directories.UserAvatarDirectory400, 400);

					//
					command.AvatarName = imageName;
					if (await _userRepository.EditByUser(command))
						return new(Status.Success);

					//else must delete new uploaded images;
					_fileService.DeleteFile(command.AvatarName, Directories.UserAvatarDirectory);
					_fileService.DeleteFile(command.AvatarName, Directories.UserAvatarDirectory100);
					_fileService.DeleteFile(command.AvatarName, Directories.UserAvatarDirectory400);

					throw new Exception();
				}

				if (await _userRepository.EditByUser(command))
					return new(Status.Success);

				throw new Exception();


			}
			catch (Exception e)
			{
				return new(Status.InternalServerError, ErrorMessages.InternalServerError, "");
			}
		}

		public EditUserByUser GetForEditByUser(int userId)
		{
			try
			{
				var user = _userRepository.GetById(userId);
				if(user is null) throw new NullReferenceException();

				return new()
				{
					Id = user.Id,
					FullName = user.FullName,
					AvatarName = user.Avatar,
					AvatarFile = null,
					UserGender = user.Gender,
					biography = user.Biography
				};
			}
			catch (Exception e)
			{
				return new();
			}
		}

		public EditUserByAdmin GetForEditByAdmin(int userId)
		{
			try
			{
				var user = _userRepository.GetById(userId);
				if (user is null) throw new NullReferenceException();

				return new()
				{
					Id = user.Id,
					FullName = user.FullName,
					Mobile = user.Mobile,
					Email = user.Email,
					AvatarName = user.Avatar,
					AvatarFile = null,
					UserGender = user.Gender,
					biography = user.Biography
				};
			}
			catch (Exception e)
			{
				return new();
			}
		}

		public async Task<bool> ActivationChange(int id)
		{
			return await _userRepository.ChangeActivation(id);
		}

		public async Task<bool> BanChange(int id)
		{
			return await _userRepository.ChangeBan(id);
		}

		public bool Logout()
		{
			return _authService.Logout();
		}
	}
}
