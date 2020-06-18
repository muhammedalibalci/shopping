using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IProductService
    {
        Task<BaseResponseDto<List<Product>>> GetAllProduct(string categoryName, string include, int page);
        Task<Product> GetProduct(int id);
        Task<BaseResponseDto<string>> AddProduct(Product product, IFormFile file);
        Task<BaseResponseDto<string>> DeleteProduct(int id);
        Task<BaseResponseDto<string>> UpdateProduct(Product product);
    }
}
