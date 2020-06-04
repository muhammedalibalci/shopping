using Domain.Dto;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
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
                    getUserResponse.Data = new UserDto
                    {
                        Name = user.FirstName
                    };
                }
                else
                {
                    getUserResponse.Errors.Add("User not found.");
                }
           

            return getUserResponse;
        }
    }
}
