using System;
using Service.Services;
using Service.Services.Interfaces;

namespace EFCourseApp.Controllers
{
	public class GroupController
	{
		private readonly IGroupService _groupservice;
		public GroupController()
		{
			_groupservice = new GroupService();
		}

		public async Task CreateAsync()
		{
			
		}
	}
}

