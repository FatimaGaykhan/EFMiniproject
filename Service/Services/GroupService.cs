using System;
using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.DTOs.Responses;
using Service.Services.Helpers.Exceptions;
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
            if (group is null) throw new ArgumentNullException();

            await _groupRepository.CreateAsync(group);
            await _groupRepository.CommitAsync();

            return new ResponseObjectDto() { Message = "This group successfully created", StatusCode = 200 };

        }

        public async Task<ResponseObjectDto> DeleteAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            var data = await _groupRepository.GetAsync(e => e.Id == id);

            if (data is null) throw new NotFoundException("Data Not Found");

            await _groupRepository.DeleteAsync(data);
            await _groupRepository.CommitAsync();

            return new ResponseObjectDto() { Message = "This group successfully deleted", StatusCode = 200 };
        }

        public async Task<List<Group>> GetAllAsync()
        {
            return await _groupRepository.GetAllAsync();

        }

        public async Task<Group> GetByIdAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
            var result = await _groupRepository.GetAsync(e => e.Id == id);
            if (result is null) throw new NotFoundException("Data Not Found");
            return result;
        }

        public Task<ResponseObjectDto> UpdateAsync(Group group)
        {
            throw new NotImplementedException();
        }
    }
}

