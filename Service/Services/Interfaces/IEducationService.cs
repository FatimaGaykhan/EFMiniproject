using System;
using Domain.Models;
using Service.Services.DTOs.Educations;

namespace Service.Services.Interfaces
{
	public interface IEducationService
	{
		Task CreateAsync(Education education);
		Task DeleteAsync(int? id);
		Task UpdateAsync(Education education);
		Task<Education> GetByIdAsync(int? id);
		Task<List<Education>> GetAllAsync();
		Task<List<EducationWithGroupDto>> GetAllWithGroupsAsync();

        //GetAllGroupsByEducation,
        //SortByCreatedDate,
        //SearchByEducationName,
    }
}

