using System;
using Domain.Models;
using Service.Services.DTOs.Groups;
using Service.Services.DTOs.Responses;

namespace Service.Services.Interfaces
{
	public interface IGroupService
	{
        Task<ResponseObjectDto> CreateAsync(Group group);
        Task<ResponseObjectDto> DeleteAsync(int? id);
        Task<ResponseObjectDto> UpdateAsync(Group group);
        Task<Group> GetByIdAsync(int? id);
        Task<List<Group>> GetAllAsync();
        Task<List<Group>> SearchByGroupName(string group);
        Task<List<GroupWithEducationIdDto>> GetAllWithEducationId(int id);
        Task<List<SortCapacityDto>> SortWithCapacity(string text);
        Task<List<GroupFilterByEducationDto>> FilterByEducationNameAsync(string text);
        //FilterByEducationName,
        //SortWithCapacity
    }
}

