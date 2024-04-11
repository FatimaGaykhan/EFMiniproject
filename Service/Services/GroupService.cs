using System;
using Domain.Models;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class GroupService : IGroupService
    {
        public Task CreateAsync(Group group)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Education>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Education> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Group group)
        {
            throw new NotImplementedException();
        }
    }
}

