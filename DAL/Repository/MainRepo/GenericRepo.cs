using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Data.Repository.MainRepo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepo(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }
        // Get all without ordering
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string[]? include = null)
        {
            IQueryable<T> result = _dbSet.AsNoTracking();
            if (filter != null)
            {
                result = result.Where(filter);
            }
            result = result.AsSplitQuery();

            if (include != null)
            {
                foreach (var includeProperty in include)
                {
                    result = result.Include(includeProperty);
                }
            }
            return await result.ToListAsync();
        }
        // Get all with ordering
        public async Task<IEnumerable<T>> GetAllWithOrderingAsync(
            Expression<Func<T, bool>>? filter = null,string[]? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> result = _dbSet.AsNoTracking();

            if (filter != null)
            {
                result = result.Where(filter);
            }
            result = result.AsSplitQuery();

            if (include != null)
            {
                foreach (var includeProperty in include)
                {
                    result = result.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                result = orderBy(result);
            }
            return await result.ToListAsync();
        }
        public async Task<IEnumerable<R>> GetAllWithSelectAsync<R>(Expression<Func<T, bool>>? filter,Expression<Func<T, R>> selector,string[]? include = null)
        {
            IQueryable<T> result = _dbSet.AsNoTracking();

            if (filter != null)
            {
                result = result.Where(filter);
            }
            result = result.AsSplitQuery();

            if (include != null)
            {
                foreach (var includeProperty in include)
                {
                    result = result.Include(includeProperty);
                }
            }

            return await result.Select(selector).ToListAsync();
        }
        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null, string[]? include = null)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            query = query.AsSplitQuery();
            if (include != null)
            {
                foreach (var includeProperty in include)
                {
                    query = query.Include(includeProperty);
                }
            }
            
            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null)
        {
            if (filter == null)
            {
                return await _dbSet.AnyAsync();
            }
            else
            {
                return await _dbSet.AnyAsync(filter);
            }
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>>? filter = null)
        {
            if (filter == null)
            {
                return await _dbSet.CountAsync();
            }
            else
            {
                return await _dbSet.CountAsync(filter);
            }
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            var addRangeAsync = entities.ToList();
            await _dbSet.AddRangeAsync(addRangeAsync);
            return addRangeAsync;
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

    }
}
