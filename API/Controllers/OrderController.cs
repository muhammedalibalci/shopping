using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Service;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/orders")]
    public class OrderController : ControllerBase
    {
        private IOrderService orderService;
        private IProductService productService;
        public OrderController(IOrderService orderService,IProductService productService)
        {
            this.orderService = orderService;
            this.productService = productService;
        }

        [HttpGet("list/{id}")]
        public async Task<ActionResult<List<Order>>> GetAllOrder([FromRoute] int id)
        {
            var products = await orderService.GetAllOrder(id,"User");
            return products.Data;
        }
        [HttpPost("add/{productId}")]
        public async Task<ActionResult<BaseResponseDto<string>>> AddOrder([FromBody] Order order,[FromRoute] int productId)
        {
            var userId = HttpContext.User.Identity.Name;
            var result = await orderService.AddOrder(order, Convert.ToInt32(userId), productId);
            if (result.HasError)
            {
                return BadRequest(result);
            }
            return result;
        }
        [HttpPost("delete/{id}")]
        public async Task<ActionResult<string>> DeleteOrder([FromRoute] int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }

            var result = await orderService.DeleteOrder(id);
            if (result.HasError)
            {
                return NotFound("There isn't such id");
            }
            return result.Data;
        }
        [HttpPost("update")]
        public async Task<ActionResult<BaseResponseDto<string>>> UpdateOrder([FromBody] Order order)
        {
            var result = await orderService.UpdateOrder(order);
            if (result.HasError)
            {
                return BadRequest(result);
            }
            return result;
        }
    }
}
