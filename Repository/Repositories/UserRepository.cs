using System;
using Domain.Models;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
	public class UserRepository:BaseRepository<User>,IUserRepository
	{
		
	}
}

