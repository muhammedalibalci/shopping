using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        Task<BaseResponseDto<UserDto>> GetAsync(int id);
    }
}
