using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MengYu.BudgetTracker.ApplicationCore.RepositoryInterfaces
{
    public interface IAsyncRepository<T> where T: class  // generic constraints
    {
        // contains common CRUD oprations
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null);
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter = null);
    }
}
