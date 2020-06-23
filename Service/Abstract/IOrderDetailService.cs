using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetail>> GetAll(int id);
        Task<BaseResponseDto<string>> Add(OrderDetail orderDetail);
    }
}
