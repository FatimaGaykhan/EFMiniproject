﻿using System;
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
        Task<List<Education>> SearchByEducationNameAsync(string education);
        Task<List<EducationWithGroupDto>> GetAllWithGroupsAsync();
        Task<List<SortCreatedDateDto>> SortWithCreatedDateAsync(string text);

    }
}

