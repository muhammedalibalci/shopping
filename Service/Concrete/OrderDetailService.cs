using Domain.Dto;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class OrderDetailService : IOrderDetailService
    {
        private IRepository<OrderDetail> _repository;
        public OrderDetailService(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }
        public async Task<BaseResponseDto<string>> Add(OrderDetail orderDetail)
        {
            try
            {
                BaseResponseDto<string> response = new BaseResponseDto<string>();
                var result = GetAll(43).Result;
                await _repository.CreateAsync(orderDetail);
                response.Data = "Added Succefully";
                return response;
            }
            catch (Exception e)
            {

                throw;
            }
       
        }

        public async Task<List<OrderDetail>> GetAll(int id)
        {
            var result = await _repository.GetListWhereAsync(x => x.OrderId == id);
            return (List<OrderDetail>)result;
        }

    }
}
