using System;
using System.Drawing;
using Domain.Models;
using Service.Services;
using Service.Services.DTOs.Groups;
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

        public async Task DeleteAsync()
        {
            try
            {
                List<Group> groups = await _groupService.GetAllAsync();

                if (groups.Count == 0) throw new EmptyException(ResponseMessages.NotAddedYet);

                foreach (Group group in groups)
                {
                    string response = $"Group Id: {group.Id}, Group: {group.Name}, Capacity: {group.Capacity}";
                    Console.WriteLine(response);
                }

                ConsoleColor.Cyan.WriteConsole("Add Group Id:");

                Id: string insertedGroupId = Console.ReadLine();

                string groupIdStr = insertedGroupId.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(groupIdStr))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                    goto Id;
                }

                int id;
                string symbols = "!@#$%^&*()_+-=[]{}|;:',.<>?";

                bool isCorrectIdFormat = int.TryParse(groupIdStr, out id);

                bool isLetterById = groupIdStr.Any(char.IsLetter);

                if (isLetterById)
                {
                    ConsoleColor.Red.WriteConsole("Id format is wrong.Please add again");
                    goto Id;
                }


                bool isSymbolById = groupIdStr.ContainsSymbol(symbols);


                if (isSymbolById)
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
                    var data = await _groupService.GetByIdAsync(id);
                    if (data is not null)
                    {
                        await _groupService.DeleteAsync(id);
                        ConsoleColor.Green.WriteConsole("Data successfully deleted");
                    }
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Id format is wrong. Please add again");
                    goto Id;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);

            }
        }

        public async Task UpdateAsync()
        {
            try
            {
                ConsoleColor.Cyan.WriteConsole("Type the Id of the data you want to change");

                List<Group> groups = await _groupService.GetAllAsync();
                List<Education> educations = await _educationService.GetAllAsync();

                if (educations.Count == 0) throw new EmptyException(ResponseMessages.NotAddedYet);

                if (groups.Count == 0) throw new EmptyException(ResponseMessages.NotAddedYet);

                foreach (Group group in groups)
                {
                    string response = $"Group Id: {group.Id}, Group: {group.Name}, Capacity: {group.Capacity}, Education Id: {group.EducationId}";
                    Console.WriteLine(response);
                }

                Id: string insertedId = Console.ReadLine();

                string strId = insertedId.Trim();

                if (string.IsNullOrWhiteSpace(strId))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again.");
                    goto Id;
                }

                int id;

                bool isCorrectIdFormat = int.TryParse(strId, out id);

                if (!isCorrectIdFormat)
                {
                    ConsoleColor.Red.WriteConsole("Id format is wrong.Please add again");
                    goto Id;
                }

                string symbols = "!@#$%^&*()_+-=[]{}|;:',.<>?";

                bool isSymbolById = strId.ContainsSymbol(symbols);

                if (isSymbolById)
                {
                    ConsoleColor.Red.WriteConsole("Id format is wrong.Please add again");
                    goto Id;
                }

                if (id == 0)
                {
                    ConsoleColor.Red.WriteConsole("Id cannot be eqaul to 0 or negative.Please add again");
                    goto Id;
                }

                if (isCorrectIdFormat)
                {
                    Group response = await _groupService.GetByIdAsync(id);

                    if (response is null) throw new NotFoundException(ResponseMessages.DataNotFound);

                    ConsoleColor.Cyan.WriteConsole("Type the group name:");

                    Name: string insertedName = Console.ReadLine();


                    string name = insertedName.Trim().ToLower();

                    if (response.Name == name)
                    {
                        ConsoleColor.Red.WriteConsole("Data already exists.Please add again");
                        goto Name;
                    }

                    if (name == "")
                    {
                        response.Name = response.Name;

                    }
                    else
                    {
                        response.Name = name;
                    }

                    ConsoleColor.Cyan.WriteConsole("Type the group capacity:");

                    Capacity: string insertedCapacity = Console.ReadLine();
                    string capacity = insertedCapacity.Trim().ToLower();

                    if (capacity == "0")
                    {
                        ConsoleColor.Red.WriteConsole("Capacity cannot be eqault to 0.Please Add again");
                        goto Capacity;
                    }

                    int capacityNum;

                    bool isCorrectCapacityFormat = int.TryParse(capacity, out capacityNum);

                    bool isCorrectFormatByCapacity = capacity.Any(char.IsLetter);


                    bool isSymbolByCapacity = capacity.ContainsSymbol(symbols);

                    if (isCorrectFormatByCapacity)
                    {
                        ConsoleColor.Red.WriteConsole("Capacity format is wrong.Please add again");
                        goto Capacity;
                    }

                    if (isSymbolByCapacity)
                    {
                        ConsoleColor.Red.WriteConsole("Capacity format is wrong.Please add again");
                        goto Capacity;
                    }


                    if (response.Capacity == capacityNum)
                    {
                        ConsoleColor.Red.WriteConsole("Data already exists.Please add again");
                        goto Capacity;
                    }
                    if (capacityNum == 0)
                    {
                        response.Capacity = response.Capacity;
                    }
                    else
                    {
                        response.Capacity = capacityNum;
                    }

                    ConsoleColor.Cyan.WriteConsole("Type the Education Id:");

                    foreach (Education education in educations)
                    {
                        string result = $" Education Id: {education.Id},Education : {education.Name}";
                        Console.WriteLine(result);
                    }

                    EducationId: string insertedEducationId = Console.ReadLine();

                    string strEducationId = insertedEducationId.Trim();

                    if (strEducationId=="")
                    {
                        response.EducationId = response.EducationId;

                    }
                    else
                    {
                        int educationId;

                        bool isCorrectEducationIdFormat = int.TryParse(strEducationId, out educationId);

                       

                        Education educationResult = await _educationService.GetByIdAsync(educationId);

                        if (educationResult is null) throw new NotFoundException(ResponseMessages.DataNotFound);

                        //if (!isCorrectEducationIdFormat)
                        //{
                        //    ConsoleColor.Red.WriteConsole("Id format is wrong.Please add again");
                        //    goto EducationId;
                        //}

                        bool isSymbolByEducationId = strEducationId.ContainsSymbol(symbols);

                        if (isSymbolByEducationId)
                        {
                            ConsoleColor.Red.WriteConsole("Id format is wrong.Please add again");
                            goto EducationId;
                        }



                        if (response.EducationId == educationId)
                        {
                            ConsoleColor.Red.WriteConsole("Data already exists.Please add again");
                            goto EducationId;
                        }

                        if (educationId == 0)
                        {
                            response.EducationId = response.EducationId;
                        }
                        else
                        {
                            response.EducationId = educationId;

                        }

                    }




                    await _groupService.UpdateAsync(response);
                    ConsoleColor.Green.WriteConsole("Data successfully updated");

                }

            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }

        public async Task SearchByNameAsync()
        {
            try
            {
                ConsoleColor.Cyan.WriteConsole("Add group name:");
                GroupName: string insertedGroupName = Console.ReadLine();

                string groupName = insertedGroupName.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(groupName))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                    goto GroupName;
                }
                else
                {
                    List<Group> groups = await _groupService.SearchByGroupName(groupName);

                    if (groups.Count == 0) throw new NotFoundException(ResponseMessages.DataNotFound);

                    foreach (Group group in groups)
                    {
                        string data = $"Id: {group.Id}, Group Name : {group.Name}, Group Capacity : {group.Capacity}";
                        Console.WriteLine(data);
                    }


                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);

            }
        }

        public async Task GetAllWithEducationId()
        {
            try
            {

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

                        List<GroupWithEducationIdDto> result = await _groupService.GetAllWithEducationId(id);

                        if (result.Count==0)
                        {   
                           ConsoleColor.Red.WriteConsole("Data Not Found");

                        }

                        foreach (var item in result)
                        {
                        ConsoleColor.Cyan.WriteConsole($"Groups:{item.Group}");

                        }



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

