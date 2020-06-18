using Domain.Dto;
using Domain.Interfaces;
using Domain.Models;
using Domain.Validations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Service.Abstract;
using Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class ProductService : IProductService
    {
        private const int pageSize = 5;
        private IRepository<Product> _repository;
        private  IWebHostEnvironment _hostingEnvironment;

        public ProductService(IRepository<Product> repository, IWebHostEnvironment hostingEnvironment)
        {
            _repository = repository;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<BaseResponseDto<List<Product>>> GetAllProduct(string categoryName,string include, int page)
        {
            BaseResponseDto<List<Product>> productResponse = new BaseResponseDto<List<Product>>();
            var products = await _repository.GetListWhereAsync(x=>x.Category.Name == categoryName,include);
            var result = LinqExtensions.GetPaged(products, page, pageSize);
            productResponse.Data = (List<Product>)result.Results;
            return productResponse;
        }
        public async Task<Product> GetProduct(int id)
        {
            return await _repository.GetAsync(id);
        }
        public async Task<BaseResponseDto<string>> AddProduct(Product product,IFormFile file)
        {
            try
            {
                BaseResponseDto<string> productResponse = new BaseResponseDto<string>();
                var result = new ProductValidator().Validate(product);
                if (!result.IsValid)
                {
                    foreach (var validationFailure in result.Errors)
                    {
                        productResponse.Errors.Add(validationFailure.PropertyName, validationFailure.ErrorMessage);
                    }
                    return productResponse;
                }
                bool resultFileUpload = FileConfiguration.FileUpload(file);
                if (!resultFileUpload)
                {
                    productResponse.Errors.Add("File Upload :","While upload file, occured an error");
                    return productResponse;
                }
                product.Image = file.FileName;
                await _repository.CreateAsync(product);
                productResponse.Data = "Added product succesfully";
                return productResponse;
            }
            catch (Exception e)
            {
                return CreateThrowMessage(e);
            }

        }

      

        public async Task<BaseResponseDto<string>> DeleteProduct(int id)
        {
            try
            {
                BaseResponseDto<string> productResponse = new BaseResponseDto<string>();
                await _repository.DeleteAsync(id);
                productResponse.Data = "Deleted product succesfully";
                return productResponse;
            }
            catch (Exception e)
            {
                return CreateThrowMessage(e);
            }

        }
        public async Task<BaseResponseDto<string>> UpdateProduct(Product product)
        {
            try
            {
                BaseResponseDto<string> productResponse = new BaseResponseDto<string>();
                var result = new ProductValidator().Validate(product);
                if (!result.IsValid)
                {
                    foreach (var validationFailure in result.Errors)
                    {
                        productResponse.Errors.Add("Message", validationFailure.ErrorMessage);
                    }
                    return productResponse;
                }
                await _repository.UpdateAsync(product);
                productResponse.Data = "Updated product succesfully";
                return productResponse;
            }
            catch (Exception e)
            {
                return CreateThrowMessage(e);
            }

        }
        private BaseResponseDto<string> CreateThrowMessage(Exception e)
        {
            BaseResponseDto<string> productResponse = new BaseResponseDto<string>();
            productResponse.Errors.Add("Message", e.Message);
            return productResponse;
        }


    }

}
