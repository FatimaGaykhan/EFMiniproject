using System;
using System.Linq.Expressions;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public BaseRepository()
        {
            _context = new AppDbContext();
            _table = _context.Set<T>();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async  Task CreateAsync(T entity)
        {
             await _table.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
             _table.Remove(entity);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            IQueryable<T> query = _table;
            if (includes.Length > 0)
            {
                query = GetAllIncludes(includes);
            }
            return predicate == null ? await query.ToListAsync() : await query.Where(predicate).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            IQueryable<T> query = _table;
            if (includes.Length > 0)
            {
                query = GetAllIncludes(includes);
            }
            return predicate == null ? await query.FirstOrDefaultAsync() : await query.FirstOrDefaultAsync(predicate);
        }

        public async Task UpdateAsync(T entity)
        {
            _table.Update(entity);
        }

        public IQueryable<T> GetAllIncludes(params string[] includes)
        {
            IQueryable<T> query = _table;
            foreach (var include in includes)
            {
                query=query.Include(include);
            }
            return query;
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? false : await _table.AnyAsync(predicate);
        }
    }
}

