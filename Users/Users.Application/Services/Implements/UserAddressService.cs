using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Users.Application.Dtos.UserAddressDtos;
using Users.Application.Services.Interfaces;
using Users.Domain.UserAgg;

namespace Users.Application.Services.Implements
{
    internal class UserAddressService : IUserAddressService
	{

		private readonly IUserAddressRepository _userAddressRepository;

		public UserAddressService(IUserAddressRepository userAddressRepository)
		{
			_userAddressRepository = userAddressRepository;
		}

		public OperationResult Create(CreateUserAddressDto dto, int userId)
		{

			try
			{

				var userAddress = new UserAddress(dto.StateId, dto.CityId, dto.AddressDetail, dto.PostalCode, dto.Phone,
					dto.FullName, dto.IranCode, userId);

				if(_userAddressRepository.Insert(userAddress))
			     	return new(Status.Success);

				throw new Exception();

			}
			catch (Exception e)
			{
				return new(Status.InternalServerError, ErrorMessages.InternalServerError);
			}

		}

		public OperationResult Delete(int addressId, int userId)
		{
			try
			{
				var userAddress = _userAddressRepository.GetBy(x => x.Id == addressId && x.UserId == userId);

				if(userAddress == null)
					return new(Status.NotFound,ErrorMessages.UserAddressNotFound);

				if(_userAddressRepository.Delete(userAddress))
					return new(Status.Success);

				throw new Exception();
			}
			catch (Exception e)
			{
				return new(Status.InternalServerError, ErrorMessages.InternalServerError);
			}
		}
	}
}
