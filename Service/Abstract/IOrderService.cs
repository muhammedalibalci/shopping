using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IOrderService
    {
        Task<BaseResponseDto<List<Order>>> GetAllOrder(int id, string include);
        Task<BaseResponseDto<string>> AddOrder(Order order,int userId, int productId);
        Task<BaseResponseDto<string>> DeleteOrder(int id);
        Task<BaseResponseDto<string>> UpdateOrder(Order product);
    }
}
