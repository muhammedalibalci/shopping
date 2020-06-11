using Domain.Dto;
using Domain.Interfaces;
using Domain.Models;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class CategoryService : ICategoryService
    {
        IRepository<Category> _repository;
        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }
        public async Task<Category> GetCategory(int id)
        {
            return await _repository.GetAsync(id);
        }
    }
}
