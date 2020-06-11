using Domain.Dto;
using Domain.Models;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
   public interface IAuthService
    {
        Task<User> GetByEmailAsync(string email);
        Task<BaseResponseDto<User>> Register(User user);
        Task<BaseResponseDto<UserDto>> Login(AuthDto auth);
        Task<Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
