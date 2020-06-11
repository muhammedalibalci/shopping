using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(string include);
        Task<IEnumerable<T>> GetAllAsync(string firstInclude, string secondInclude);
        Task<IEnumerable<T>> GetListWhereAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetListWhereAsync(Expression<Func<T, bool>> predicate,string include);
        Task<T> GetAsync(int id);
        Task<T> GetWhereAsync(Expression<Func<T, bool>> predicate);
    }
}
