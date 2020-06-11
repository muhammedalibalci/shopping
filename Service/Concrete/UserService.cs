using Domain.Dto;
using Domain.Interfaces;
using Domain.Models;
using Domain.Validations;
using Microsoft.Extensions.DependencyInjection;
using Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {

            _userRepository = userRepository;
        }
        public async Task<BaseResponseDto<UserDto>> GetAsync(int id)
        {
            BaseResponseDto<UserDto> getUserResponse = new BaseResponseDto<UserDto>();
            User user = await _userRepository.GetAsync(id);
            if (user != null)
            {
                UserDto userDto = new UserDto(user.Id, user.FirstName, user.LastName, user.Address, user.AccessToken);
                getUserResponse.Data = userDto;
            }
            else
            {
                getUserResponse.Errors.Add("User", "User not found.");
            }
            return getUserResponse;
        }

    }
}
