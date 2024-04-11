using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Service.Services.DTOs.Educations;
using Service.Services.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class EducationService : IEducationService
    {
        private readonly AppDbContext _context;
        public EducationService()
        {
            _context = new AppDbContext();
        }


        public async Task CreateAsync(Education education)
        {
            if ( education is null) throw new ArgumentNullException();

            await _context.Educations.AddAsync(education);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            var data =  await _context.Educations.FirstOrDefaultAsync(m => m.Id == id);

            if (data is null) throw new NotFoundException("Data Not Found");

            _context.Educations.Remove(data);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Education>> GetAllAsync()
        {
            return await _context.Educations.ToListAsync();
        }

        public Task<List<EducationWithGroupDto>> GetAllWithGroupsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Education> GetByIdAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
            var result = await _context.Educations.FirstOrDefaultAsync(m => m.Id == id);
            if (result is null) throw new NotFoundException("Data Not Found");
            return result;

        }

        public Task UpdateAsync(Education education)
        {
            throw new NotImplementedException();
        }
    }
}

