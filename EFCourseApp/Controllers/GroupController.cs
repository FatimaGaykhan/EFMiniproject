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

                List<Education> educations = await _educationService.GetAllAsync();

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
	}
}

