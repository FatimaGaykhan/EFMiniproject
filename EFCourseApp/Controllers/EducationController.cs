using System;
using System.Text.RegularExpressions;
using Domain.Models;
using Service.Services;
using Service.Services.Helpers.Constants;
using Service.Services.Helpers.Exceptions;
using Service.Services.Helpers.Extensions;
using Service.Services.Interfaces;

namespace EFCourseApp.Controllers
{
	public class EducationController
	{
		private readonly IEducationService _educationService;
		public EducationController()
		{
			_educationService = new EducationService();
		}

		public async Task Create()
		{
			try
			{
                Console.WriteLine("Add education name:");

                Name: string str1 = Console.ReadLine();

				string educationName = str1.Trim().ToLower();


                if (string.IsNullOrWhiteSpace(educationName))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                    goto Name;
                }


                List<Education> response = await _educationService.GetAllAsync();

                foreach (Education education in response)
                {
                    if (education.Name == educationName)
                    {
                        ConsoleColor.Red.WriteConsole("Education name already exists.Please add again");
                        goto Name;
                    }
                }

                string symbols = "!@#$%^&*()_+-=[]{}|;:',.<>?";

                bool isSymbolByName = educationName.ContainsSymbol(symbols);


                if (isSymbolByName)
                {
                    ConsoleColor.Red.WriteConsole("Education name format is wrong.Please add again");
                    goto Name;
                }

                bool isNumericByEducation = educationName.Any(char.IsDigit);
				if (isNumericByEducation)
				{
                    ConsoleColor.Red.WriteConsole("Education name format is wrong.Please add again");
                    goto Name;
                }


                Console.WriteLine("Add color:");

				Color: string str2 = Console.ReadLine();

                string color = str2.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(color))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                    goto Color;
                }

                foreach (Education education in response)
                {
                    if (education.Color == color)
                    {
                        ConsoleColor.Red.WriteConsole("Color already exists.Please add again");
                        goto Color;
                    }
                }

                bool isSymbolByColor = color.ContainsSymbol(symbols);

                if (isSymbolByColor)
                {
                    ConsoleColor.Red.WriteConsole("Color format is wrong.Please add again");
                    goto Color;
                }


                bool isNumericFormatByColor = color.Any(char.IsDigit);

                if (isNumericFormatByColor)
                {
                    ConsoleColor.Red.WriteConsole("Color format is wrong.Please add again");
                    goto Color;
                }


                await _educationService.CreateAsync(new Education { Name = educationName, Color = color, CreatedDate = DateTime.Now });

            }
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
            }
        }

        public async Task GetAllAsync()
        {
            try
            {
                List<Education> educations = await _educationService.GetAllAsync();
                if (educations.Count == 0) throw new EmptyException(ResponseMessages.NotAddedYet);
                foreach (Education education in educations)
                {
                    string response = $"Education: {education.Name}, Color: {education.Color}";
                    Console.WriteLine(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteAsync()
        {
            try
            {
                List<Education> educations = await _educationService.GetAllAsync();

                if (educations.Count == 0) throw new EmptyException(ResponseMessages.NotAddedYet);

                foreach (Education education in educations)
                {
                    string response = $"Education Id: {education.Id}, Education: {education.Name}, Color: {education.Color}";
                    Console.WriteLine(response);
                }

                ConsoleColor.Cyan.WriteConsole("Add Education Id:");

            Id: string insertedEducationId = Console.ReadLine();

                string educationIdStr = insertedEducationId.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(educationIdStr))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                    goto Id;
                }

                int id;

                string symbols = "!@#$%^&*()_+-=[]{}|;:',.<>?";


                bool isCorrectIdFormat = int.TryParse(educationIdStr, out id);

                bool isLetterById = educationIdStr.Any(char.IsLetter);

                if (isLetterById)
                {
                    ConsoleColor.Red.WriteConsole("Id format is wrong.Please add again");
                    goto Id;
                }

                bool isSymbolById = educationIdStr.ContainsSymbol(symbols);

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
                    var data = await _educationService.GetByIdAsync(id);
                    if (data is not null)
                    {
                        await _educationService.DeleteAsync(id);
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

        public async Task GetByIdAsync()
        {
            try
            {
                List<Education> educations = await _educationService.GetAllAsync();

                if (educations.Count == 0) throw new EmptyException(ResponseMessages.NotAddedYet);

                foreach (var education in educations)
                {
                    Console.WriteLine($"Education id:{education.Id} Education :{education.Name}");
                }

                ConsoleColor.Cyan.WriteConsole("Add Id:");

            Id: string insertedId = Console.ReadLine();

                string idStr = insertedId.Trim();

                if (string.IsNullOrWhiteSpace(idStr))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.PLease add again");
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

                    Education education = await _educationService.GetByIdAsync(id);

                    if (education is null) throw new NotFoundException(ResponseMessages.DataNotFound);

                    string data = $"Id: {education.Id},  Education : {education.Name}, Education color : {education.Color}";

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

        public async Task UpdateAsync()
        {
            try
            {
                ConsoleColor.Cyan.WriteConsole("Type the Id of the data you want to change");

                List<Education> educations = await _educationService.GetAllAsync();

                if (educations.Count == 0) throw new EmptyException(ResponseMessages.NotAddedYet);

                foreach (Education education in educations)
                {
                    string response = $"Education Id: {education.Id}, Education: {education.Name}, Color: {education.Color}";
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

                    Education response = await _educationService.GetByIdAsync(id);

                    if (response is null) throw new NotFoundException(ResponseMessages.DataNotFound);

                    ConsoleColor.Cyan.WriteConsole("Type the education name:");

                Name: string insertedName = Console.ReadLine();

                    string name = insertedName.Trim().ToLower();


                    bool isNumericByName = name.Any(char.IsDigit);

                    bool isSymbolByName = name.ContainsSymbol(symbols);

                    if (isSymbolByName)
                    {
                        ConsoleColor.Red.WriteConsole("Education name format is wrong. Please add again");
                        goto Name;
                    }

                    if (isNumericByName)
                    {
                        ConsoleColor.Red.WriteConsole("Education name format is wrong. Please add again");
                        goto Name;
                    }


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

                    ConsoleColor.Cyan.WriteConsole("Type the education color:");

                Color: string insertedColorName = Console.ReadLine();
                    string colorName = insertedColorName.Trim().ToLower();
                    bool isNumeric = colorName.Any(char.IsDigit);
                    bool isCorrectFormatByColorName = colorName.All(char.IsLetter);

                    if (isNumeric)
                    {
                        ConsoleColor.Red.WriteConsole("Color name format is wrong. Please add again");
                        goto Color;
                    }

                    if (!isCorrectFormatByColorName)
                    {
                        ConsoleColor.Red.WriteConsole("Color name format is wrong. Please add again");
                        goto Color;
                    }



                    if (response.Color == colorName)
                    {
                        ConsoleColor.Red.WriteConsole("Data already exists.Please add again");
                        goto Color;
                    }
                    if (colorName == "")
                    {
                        response.Color = response.Color;
                    }
                    else
                    {
                        response.Color = colorName;
                    }

                    await _educationService.UpdateAsync(response);

                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Id format is wrong. Please add again");
                    goto Id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task SearchByName()
        {

        }

    }
}

