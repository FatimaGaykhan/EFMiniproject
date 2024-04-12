﻿using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.DTOs.Educations;
using Service.Services.DTOs.Responses;
using Service.Services.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepository;
        public EducationService()
        {
            _educationRepository = new EducationRepository();
        }


        public async Task<ResponseObjectDto> CreateAsync(Education education)
        {
            if (education is null) throw new ArgumentNullException();

            await _educationRepository.CreateAsync(education);
            await _educationRepository.CommitAsync();

            return new ResponseObjectDto() { Message = "This group successfully created", StatusCode = 200 };

        }

        public async Task<ResponseObjectDto> DeleteAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            var data = await _educationRepository.GetAsync(e => e.Id == id);

            if (data is null) throw new NotFoundException("Data Not Found");

            await _educationRepository.DeleteAsync(data);
            await _educationRepository.CommitAsync();

            return new ResponseObjectDto() { Message = "This group successfully deleted", StatusCode = 200 };

        }

        public async Task<List<Education>> GetAllAsync()
        {
            return await _educationRepository.GetAllAsync();
        }

        //public async Task<List<EducationWithGroupDto>> GetAllWithGroupsAsync()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<Education> GetByIdAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
            var result = await _educationRepository.GetAsync(e => e.Id == id);
            if (result is null) throw new NotFoundException("Data Not Found");
            return result;

        }

        public async Task<ResponseObjectDto> UpdateAsync(Education education)
        {
            if (education is null) throw new ArgumentNullException();

            Education existingEducation = await _educationRepository.GetAsync(e => e.Id == education.Id);

            if (existingEducation is null)
            {
                return new ResponseObjectDto { Message = "Education not found",StatusCode=400 };
            }

            existingEducation.Name = education.Name;
            existingEducation.Color = education.Color;
            existingEducation.CreatedDate = DateTime.Now;


            await _educationRepository.UpdateAsync(existingEducation);
            await _educationRepository.CommitAsync();


            return new ResponseObjectDto {  Message = "Education updated successfully", StatusCode = 200 };

        }
    }
}

