using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Application.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        void Add(T entity);

        Task<bool> SaveChangesAsync();

        Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate);
    }
}