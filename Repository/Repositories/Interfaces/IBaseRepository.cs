using System;
using System.Linq.Expressions;
using Domain.Common;

namespace Repository.Repositories.Interfaces
{
	public interface IBaseRepository<T> where T:BaseEntity
	{
		Task CreateAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
		Task<T> GetAsync(Expression<Func<T, bool>> predicate = null, params string[] includes);
		Task<List<T>> GetAllAsync(Expression<Func<T,bool>> predicate=null,params string[] includes);
		Task<int> CommitAsync();
		Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate = null);
	
    }
}

