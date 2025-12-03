using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.MainRepo
{
    public interface IGenericRepo<T> where T : class
    {
        Task<T?> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string[]? include = null);
        Task<IEnumerable<T>> GetAllWithOrderingAsync(Expression<Func<T, bool>>? filter = null, string[]? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null, string[]? include = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null);

        Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);

        Task AddAsync(T entity);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);



    }
}
