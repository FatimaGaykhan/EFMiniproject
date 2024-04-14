using System;
namespace Service.Services.DTOs.Groups
{
	public class GroupFilterByEducationDto
	{
		public string Education { get; set; }

		public List<string> Groups { get; set; }

		public GroupFilterByEducationDto()
		{
			Groups = new();
		}
	}
}

