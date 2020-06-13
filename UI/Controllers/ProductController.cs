using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class ProductController : ControllerBase
    {
        private IProductService productService;
        private IWebHostEnvironment _hostingEnvironment;
        public ProductController(IProductService productService, IWebHostEnvironment hostingEnvironment)
        {
            this.productService = productService;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get([FromRoute] int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }
            var product = await productService.GetProduct(id);
            return product;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProduct([FromQuery] int page, [FromQuery] string category)
        {
            if (string.IsNullOrEmpty(page.ToString()))
            {
                return BadRequest();
            }
            var products = await productService.GetAllProduct(category, "Category", page);
            return products.Data;
        }
        [HttpPost("add")]
        public async Task<ActionResult<BaseResponseDto<string>>> AddProduct([FromForm] Product product)
        {
            var file = Request.Form.Files[0];
            var result = await productService.AddProduct(product,file);
            if (result.HasError)
            {
                return BadRequest(result);
            }
            return result;
        }
        [HttpPost("delete/{id}")]
        public async Task<ActionResult<string>> DeleteProduct([FromRoute] int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }

            var result = await productService.DeleteProduct(id);
            if (result.HasError)
            {
                return NotFound("There isn't such id");
            }
            return result.Data;
        }
        [HttpPost("update")]
        public async Task<ActionResult<BaseResponseDto<string>>> UpdateProduct([FromBody] Product product)
        {
            var result = await productService.UpdateProduct(product);
            if (result.HasError)
            {
                return BadRequest(result);
            }
            return result;
        }

    }
}
