using System;
using System.Drawing;
using Domain.Models;
using Service.Services;
using Service.Services.Helpers.Extensions;
using Service.Services.Interfaces;

namespace EFCourseApp.Controllers
{
	public class UserController
	{
		private readonly IUserService _userService;
        public bool Login { get; set; }

        public UserController()
		{
			_userService = new UserService();
		}

		public async Task RegisterAsync()
		{
			try
			{
                Console.WriteLine("Add Full Name:");

                FullName: string insertedFullName = Console.ReadLine();

                string fullName = insertedFullName.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                    goto FullName;
                }

                bool isValidFullName = fullName.ValidateFullName();
                if (!isValidFullName)
                {
                    ConsoleColor.Red.WriteConsole("Full Name Format Is Wrong.Please add again");
                    goto FullName;
                }

                //string symbols = "!@#$%^&*()_+-=[]{}|;:',.<>?";

                //bool isSymbolByFullName = fullName.ContainsSymbol(symbols);


                //if (isSymbolByFullName)
                //{
                //    ConsoleColor.Red.WriteConsole("Full Name format is wrong.Please add again");
                //    goto FullName;
                //}

                //bool isNumericByFullName = fullName.Any(char.IsDigit);
                //if (isNumericByFullName)
                //{
                //    ConsoleColor.Red.WriteConsole("FullName format is wrong.Please add again");
                //    goto FullName;
                //}





                Console.WriteLine("Add Username:");

                UserName: string insertedUsername = Console.ReadLine();

                string username = insertedUsername.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(username))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                    goto UserName;
                }

                List<User> response = await _userService.GetAllAsync();

                foreach (User user in response)
                {
                    if (user.Username== username)
                    {
                        ConsoleColor.Red.WriteConsole("Username already exists.Please add again");
                        goto UserName;
                    }
                }


                bool isCorrectUsername = username.ValidateUsername();

                if (!isCorrectUsername)
                {
                    ConsoleColor.Red.WriteConsole("Must be in Username:   Alphanumeric characters and underscores, length between 3 and 20 characters.Please Add Again");
                    goto UserName;
                }




                Console.WriteLine("Add Email:");

                Email: string insertedEmail = Console.ReadLine();

                string email = insertedEmail.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(email))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                    goto Email;
                }

                

                foreach (User user in response)
                {
                    if (user.Email == email)
                    {
                        ConsoleColor.Red.WriteConsole("Email already exists.Please add again");
                        goto Email;
                    }
                }


                bool isCorrectEmail = email.ValidateEmail();

                if (!isCorrectEmail)
                {
                    ConsoleColor.Red.WriteConsole("Please Add Again . Email example : email@example.com ");
                    goto Email;
                }




                Console.WriteLine("Add Password:");

                Password: string insertedPassword = Console.ReadLine();

                string password = insertedPassword.Trim();

                if (string.IsNullOrWhiteSpace(password))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                    goto Password;
                }



                foreach (User user in response)
                {
                    if (user.Password== password)
                    {
                        ConsoleColor.Red.WriteConsole("Password already exists.Please add again");
                        goto Password;
                    }
                }


                bool isCorrectPassword = password.ValidatePassword();

                if (!isCorrectPassword)
                {
                    ConsoleColor.Red.WriteConsole("Must be in password : at least 8 characters long, with at least one uppercase letter, one lowercase letter, and one digit");
                    goto Password;
                }

                await _userService.RegisterAsync(new User { FullName= fullName, Username = username,Email=email,Password=password, CreatedDate = DateTime.Now });



            }
            catch (Exception ex)
			{
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }

        public async Task LoginAsync()
        {
            try
            {
                ConsoleColor.Cyan.WriteConsole("Add Username or Email:");
                UserOrEmail: string insertedUserOrEmail = Console.ReadLine();
                string userOrEmail = insertedUserOrEmail.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(userOrEmail))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                    goto UserOrEmail;
                }



                ConsoleColor.Cyan.WriteConsole("Add Password:");
                Password: string password = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(password))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty.Please add again");
                    goto Password;
                }

                 Login = await _userService.LoginAsync(userOrEmail, password);
                if (Login == false)
                {
                    ConsoleColor.Red.WriteConsole("Login Failed");
                }
                else
                {
                    ConsoleColor.Green.WriteConsole("Login Succes");

                }

                //Console.WriteLine(result);

            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);

            }
        }
	}
}

