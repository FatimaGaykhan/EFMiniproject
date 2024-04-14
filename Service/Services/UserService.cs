using System;
using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.DTOs.Responses;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<bool> LoginAsync(string userOrEmail, string password)
        {
            var users = await _userRepository.GetAllAsync();
          
            bool login = false;

            foreach (User user in users)
            {
                if (user.Email == userOrEmail || user.Username == userOrEmail)
                {
                    if (user.Password == password)
                    {
                        login = true;
                        break;
                    }
                }
                else
                {
                    login = false;
                }
            }
            return login;

        }

        public async Task RegisterAsync(User user)
        {
            if (user is null) throw new ArgumentNullException();

            await _userRepository.CreateAsync(user);
            await _userRepository.CommitAsync();

        }
    }
}

