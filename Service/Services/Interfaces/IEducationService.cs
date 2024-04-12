using System;
using Domain.Models;
using Service.Services.DTOs.Educations;
using Service.Services.DTOs.Responses;

namespace Service.Services.Interfaces
{
	public interface IEducationService
	{
        Task<ResponseObjectDto> CreateAsync(Education education);
        Task<ResponseObjectDto> DeleteAsync(int? id);
        Task<ResponseObjectDto> UpdateAsync(Education education);
        Task<Education> GetByIdAsync(int? id);
        Task<List<Education>> GetAllAsync();
        //Task<string>
        //Task<List<EducationWithGroupDto>> GetAllWithGroupsAsync();

        //GetAllGroupsByEducation,
        //SortByCreatedDate,
        
    }
}

