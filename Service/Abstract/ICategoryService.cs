using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface ICategoryService
    {
        Task<Category> GetCategory(int id);

    }
}
