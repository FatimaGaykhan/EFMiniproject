using System;
using Domain.Models;

namespace Service.Services.Interfaces
{
	public interface IGroupService
	{
        Task CreateAsync(Group group);
        Task DeleteAsync(int? id);
        Task UpdateAsync(Group group);
        Task<Education> GetByIdAsync(int? id);
        Task<List<Education>> GetAll();
        //SearchByGroupName,
        //FilterByEducationName,
        //GetAllWithEducationId,
        //SortWithCapacity
    }
}

