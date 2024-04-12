using System;
using Domain.Models;
using Service.Services.DTOs.Responses;

namespace Service.Services.Interfaces
{
	public interface IGroupService
	{
        Task<ResponseObjectDto> CreateAsync(Group group);
        Task<ResponseObjectDto> DeleteAsync(int? id);
        Task<ResponseObjectDto> UpdateAsync(Group group);
        Task<Group> GetByIdAsync(int? id);
        Task<List<Group>> GetAll();
        //SearchByGroupName,
        //FilterByEducationName,
        //GetAllWithEducationId,
        //SortWithCapacity
    }
}

