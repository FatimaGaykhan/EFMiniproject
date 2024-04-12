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

              

                bool ContainsSymbol(string str)
                {
                    string symbols = "!@#$%^&*()_+-=[]{}|;:',.<>?";

                    return str.Any(c => symbols.Contains(c));
                }
                

                if (ContainsSymbol(educationName))
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


                if (ContainsSymbol(color))
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
            bool isCorrectIdFormat = int.TryParse(educationIdStr, out id);

            bool ContainsSymbol(string str)
            {
                string symbols = "!@#$%^&*()_+-=[]{}|;:',.<>?";

                return str.Any(c => symbols.Contains(c));
            }

            if (ContainsSymbol(educationIdStr))
            {
                ConsoleColor.Red.WriteConsole("Id format is wrong.Please add again");
                goto Id;
            }

            if (isCorrectIdFormat)
            {
                try
                {
                    var data = await _educationService.GetByIdAsync(id);
                    if (data is not null)
                    {
                        await _educationService.DeleteAsync(id);
                        ConsoleColor.Green.WriteConsole("Data successfully deleted");
                    }
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Id format is wrong. Please add again");
                goto Id;
            }

        }

        public async Task GetByIdAsync()
        {
            List<Education> educations = await _educationService.GetAllAsync();

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


            if (isCorrectIdFormat)
            {
                try
                {
                    Education education = await _educationService.GetByIdAsync(id);

                    if (education is null) throw new NotFoundException(ResponseMessages.DataNotFound);

                    string data = $"Id: {education.Id},  Education : {education.Name}, Education color : {education.Color}";

                    Console.WriteLine(data);

                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);

                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Id format is wrong.Please add again");
                goto Id;
            }


        }

    }
}

