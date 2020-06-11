using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IProductService
    {
        Task<BaseResponseDto<List<Product>>> GetAllProduct(string include,int page);
        Task<Product> GetProduct(int id);
        Task<BaseResponseDto<string>> AddProduct(Product product);
        Task<BaseResponseDto<string>> DeleteProduct(int id);
        Task<BaseResponseDto<string>> UpdateProduct(Product product);
    }
}
