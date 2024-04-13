using System;
using System.Drawing;
using Domain.Models;
using Service.Services;
using Service.Services.Helpers.Constants;
using Service.Services.Helpers.Exceptions;
using Service.Services.Helpers.Extensions;
using Service.Services.Interfaces;

namespace EFCourseApp.Controllers
{
	public class GroupController
	{
		private readonly IGroupService _groupService;
        private readonly IEducationService _educationService;
		public GroupController()
		{
			_groupService = new GroupService();
            _educationService = new EducationService();
		}

		public async Task CreateAsync()
		{
			try
			{
                List<Education> educations = await _educationService.GetAllAsync();
                if (educations.Count==0)
                {
                    ConsoleColor.Red.WriteConsole("Education Not Added Yet");
                    return;
                }
                Console.WriteLine("Add group name:");

                Name: string insertedName = Console.ReadLine();

                string groupName = insertedName.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(groupName))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                    goto Name;
                }

                List<Group> response = await _groupService.GetAllAsync();

                foreach (Group group in response)
                {
                    if (group.Name == groupName)
                    {
                        ConsoleColor.Red.WriteConsole("Group name already exists.Please add again");
                        goto Name;
                    }
                }

                Console.WriteLine("Add capacity:");

                Capacity: string insertedCapacity = Console.ReadLine();

                string capacityStr = insertedCapacity.Trim();

                if (string.IsNullOrWhiteSpace(capacityStr))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.PLease add again");
                    goto Capacity;
                }

                int capacity;

                bool isCorrectCapacityFormat = int.TryParse(capacityStr, out capacity);

                if (!isCorrectCapacityFormat)
                {
                    ConsoleColor.Red.WriteConsole("Capacity format is wrong.Please add again");
                    goto Capacity;
                }

                bool isCorrectCapacityFormatForSymbol = capacityStr.All(char.IsDigit);

                if (!isCorrectCapacityFormatForSymbol)
                {
                    ConsoleColor.Red.WriteConsole("Capacity format is wrong.Please add again");
                    goto Capacity;
                }

                if (capacity == 0 || capacity < 0)
                {
                    ConsoleColor.Red.WriteConsole("Capacity cannot be eqaul to 0 or negative.Please add again");
                    goto Capacity;
                }


                Console.WriteLine("Add Education Id:");

               

                foreach (var education in educations)
                {
                    Console.WriteLine($"Education Id: {education.Id} Education: {education.Name}");
                }

                EducationId: string insertedEducationId=Console.ReadLine();

                string educationIdStr = insertedEducationId.Trim();

                if (string.IsNullOrWhiteSpace(educationIdStr))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.PLease add again");
                    goto EducationId;
                }

                int educationId;

                bool isCorrectEducationIdFormat = int.TryParse(educationIdStr, out educationId);

                bool isExist = educations.Exists(m => m.Id == educationId);
                if (!isExist)
                {
                    ConsoleColor.Red.WriteConsole("Education with this ID was not found.PLease add again");
                    goto EducationId;
                }

                if (educationId == 0 || educationId < 0)
                {
                    ConsoleColor.Red.WriteConsole("Education Id cannot be eqaul to 0 or negative.Please add again");
                    goto Capacity;
                }

                if (!isCorrectEducationIdFormat)
                {
                    ConsoleColor.Red.WriteConsole("Education Id format is wrong.Please add again");
                    goto EducationId;
                }


                bool isCorrectEducationIdFormatForSymbol = educationIdStr.All(char.IsDigit);

                if (!isCorrectEducationIdFormatForSymbol)
                {
                    ConsoleColor.Red.WriteConsole("Education Id format is wrong.Please add again");
                    goto EducationId;
                }


                await _groupService.CreateAsync(new Group { Name = groupName,Capacity=capacity, EducationId=educationId , CreatedDate = DateTime.Now });


            }
            catch (Exception ex)
			{
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
			
		}

        public async Task GetAllAsync()
        {
            try
            {
                List<Group> groups = await _groupService.GetAllAsync();
                if (groups.Count == 0) throw new EmptyException(ResponseMessages.NotAddedYet);
                foreach (Group group in groups)
                {
                    string response = $"Group name: {group.Name}";
                    Console.WriteLine(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task GetByIdAsync()
        {
            try
            {
                List<Group> groups = await _groupService.GetAllAsync();

                if (groups.Count == 0) throw new EmptyException(ResponseMessages.NotAddedYet);

                foreach (var group in groups)
                {
                    Console.WriteLine($"Group id:{group.Id} Group name :{group.Name}");
                }

                ConsoleColor.Cyan.WriteConsole("Add Id:");

                Id: string insertedId = Console.ReadLine();

                string idStr = insertedId.Trim();

                if (string.IsNullOrWhiteSpace(idStr))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                    goto Id;
                }


                int id;

                bool isCorrectIdFormat = int.TryParse(idStr, out id);

                bool isCorrectIdFormatForSymbol = idStr.All(char.IsDigit);

                if (!isCorrectIdFormatForSymbol)
                {
                    ConsoleColor.Red.WriteConsole("Id format is wrong.Please add again");
                    goto Id;
                }

                if (id == 0 || id < 0)
                {
                    ConsoleColor.Red.WriteConsole("Id cannot be eqaul to 0 or negative.Please add again");
                    goto Id;
                }


                if (isCorrectIdFormat)
                {

                    Group group = await _groupService.GetByIdAsync(id);

                    if (group is null) throw new NotFoundException(ResponseMessages.DataNotFound);

                    string data = $"Id: {group.Id},  Group : {group.Name}, Group Capacity : {group.Capacity}, Education Id:{group.EducationId}";

                    Console.WriteLine(data);

                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Id format is wrong.Please add again");
                    goto Id;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);

            }
        }

	}
}

