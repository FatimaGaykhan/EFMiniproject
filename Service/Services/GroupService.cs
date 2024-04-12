using System;
using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.DTOs.Responses;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        public GroupService()
        {
            _groupRepository = new GroupRepository();
        }
        public async Task<ResponseObjectDto> CreateAsync(Group group)
        {
            if (group is null)
            {
                return new ResponseObjectDto() { Message = "Data cannot be null", StatusCode = 400 };
            }
            else if (await _groupRepository.IsExistAsync(g => g.Name.Trim().ToLower() == group.Name.Trim().ToLower()))
            {
                return new ResponseObjectDto() { Message = "This group is exist", StatusCode = 400 };
            }

            await _groupRepository.CreateAsync(group);
            await _groupRepository.CommitAsync();

            return new ResponseObjectDto() { Message = "This group successfully created", StatusCode = 200 };

        }

        public Task<ResponseObjectDto> DeleteAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Group>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Group> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObjectDto> UpdateAsync(Group group)
        {
            throw new NotImplementedException();
        }
    }
}

