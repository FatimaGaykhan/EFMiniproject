using System;
using Domain.Common;

namespace Repository.Repositories.Interfaces
{
	public interface IBaseRepository<T> where T:BaseEntity
	{
		Task CreateAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(int? id);
		Task<T> GetByIdAsync(int? id);
		Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllWithExpression(Func<T, bool> predicate);

    }
}

