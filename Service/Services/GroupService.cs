using System;
using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.DTOs.Educations;
using Service.Services.DTOs.Groups;
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

        public async Task<List<GroupWithEducationIdDto>> GetAllWithEducationId(int id)
        {
            List<Group> groups = await _groupRepository.GetAllAsync(g=>g.EducationId==id,"Education");
            List<GroupWithEducationIdDto> groupWithEducationIdDto = new();
            foreach (Group group in groups)
            {
                groupWithEducationIdDto.Add(new GroupWithEducationIdDto() { Group = group.Name, EducationId = group.Education.Id});

                //education.Groups.Select(g => g.Name).ToList()
            }

            return groupWithEducationIdDto;
        }

        public async Task<Group> GetByIdAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
            var result = await _groupRepository.GetAsync(e => e.Id == id);
            if (result is null) throw new NotFoundException("Data Not Found");
            return result;
        }

        public async Task<List<Group>> SearchByGroupName(string group)
        {
            return await _groupRepository.GetAllAsync(m => m.Name == group.Trim() || m.Name.Contains(group.Trim()));
        }

        public async Task<List<SortCapacityDto>> SortWithCapacity(string text)
        {
            List<Group> groups = await _groupRepository.GetAllAsync();

            List<SortCapacityDto> sortCapacityDtos = new();
            if (text == "asc")
            {

                List<Group> orderedListByAsc = groups.OrderBy(g => g.Capacity).ToList();


                foreach (Group item in orderedListByAsc)
                {
                    sortCapacityDtos.Add(new SortCapacityDto() { Group = item.Name, Capacity = item.Capacity });
                }
            }

            if (text == "desc")
            {
                List<Group> orderedListByDesc = groups.OrderByDescending(g => g.Capacity).ToList();

                foreach(Group item in orderedListByDesc)
                {
                    sortCapacityDtos.Add(new SortCapacityDto() { Group = item.Name, Capacity = item.Capacity });
                }
            }

            return sortCapacityDtos;
        }

        public async Task<ResponseObjectDto> UpdateAsync(Group group)
        {
            if (group is null) throw new ArgumentNullException();

            Group existingGroup = await _groupRepository.GetAsync(e => e.Id == group.Id);

            if (existingGroup is null)
            {
                return new ResponseObjectDto { Message = "Group not found", StatusCode = 400 };
            }

            existingGroup.Name = group.Name;
            existingGroup.Capacity = group.Capacity;
            existingGroup.EducationId = group.EducationId;


            await _groupRepository.UpdateAsync(existingGroup);
            await _groupRepository.CommitAsync();


            return new ResponseObjectDto { Message = "Group updated successfully", StatusCode = 200 };
        }
    }
}

