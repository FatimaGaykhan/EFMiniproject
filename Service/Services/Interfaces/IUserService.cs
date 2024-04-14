using System;
using Domain.Models;

namespace Service.Services.Interfaces
{
	public interface IUserService
	{
		public Task RegisterAsync(User user);

        Task<List<User>> GetAllAsync();

		public Task<bool> LoginAsync(string userOrEmail,string password); 

    }
}

