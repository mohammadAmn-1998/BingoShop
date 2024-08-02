using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;
using Users.Application.Dtos.UserAddressDtos;

namespace Users.Application.Services.Interfaces
{
    public interface IUserAddressService
	{

		OperationResult Create(CreateUserAddressDto dto,int userId);

		OperationResult Delete(int addressId,int userId);

	}
}
