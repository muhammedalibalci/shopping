﻿using Domain.Dto;
using Domain.Interfaces;
using Domain.Models;
using Domain.Validations;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class OrderService : IOrderService
    {
        IRepository<Order> _repository;
        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
        }
        public async Task<BaseResponseDto<List<Order>>> GetAllOrder(int id, string include)
        {
            BaseResponseDto<List<Order>> orderResponse = new BaseResponseDto<List<Order>>();
            orderResponse.Data = (List<Order>)await _repository.GetListWhereAsync(x => x.UserId == id, include);
            return orderResponse;
        }
        public async Task<BaseResponseDto<string>> AddOrder(Order order,int userId,int productId)
        {
            try
            {
                BaseResponseDto<string> orderResponse = new BaseResponseDto<string>();
                var result = new OrderValidator().Validate(order);
                if (!result.IsValid)
                {
                    foreach (var validationFailure in result.Errors)
                    {
                        orderResponse.Errors.Add("",validationFailure.ErrorMessage);
                    }
                    return orderResponse;
                }
                order.UserId = userId;
                order.ProductId = productId;
                order.Timestamp = (int)new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                orderResponse.Data = "Added order succesfully";
                await _repository.CreateAsync(order);
                return orderResponse;
            }
            catch (Exception e)
            {
                BaseResponseDto<string> orderResponse = new BaseResponseDto<string>();
                orderResponse.Errors.Add(e.Message,"While order added, a problem occured");
                return orderResponse;
            }
        }
        public async Task<BaseResponseDto<string>> DeleteOrder(int id)
        {
            try
            {
                BaseResponseDto<string> orderResponse = new BaseResponseDto<string>();
                await _repository.DeleteAsync(id);
                orderResponse.Data = "Deleted order succesfully";
                return orderResponse;
            }
            catch (Exception e)
            {
                BaseResponseDto<string> orderResponse = new BaseResponseDto<string>();
                orderResponse.Errors.Add("Message",e.Message);
                return orderResponse;
            }
        }
        public async Task<BaseResponseDto<string>> UpdateOrder(Order order)
        {
            try
            {
                BaseResponseDto<string> orderResponse = new BaseResponseDto<string>();
                var result = new OrderValidator().Validate(order);
                if (!result.IsValid)
                {
                    foreach (var validationFailure in result.Errors)
                    {
                        orderResponse.Errors.Add("Message",validationFailure.ErrorMessage);
                    }
                    return orderResponse;
                }
                await _repository.UpdateAsync(order);
                orderResponse.Data = "Updated order succesfully";
                return orderResponse;
            }
            catch (Exception e)
            {
                BaseResponseDto<string> orderResponse = new BaseResponseDto<string>();
                orderResponse.Errors.Add("Message",e.Message);
                return orderResponse;
            }
        }
    }
}
