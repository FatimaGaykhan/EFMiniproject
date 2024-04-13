using System;
namespace Service.Services.DTOs.Educations
{
	public class EducationWithGroupDto
	{
		public string Education { get; set; }
		public List<string> Groups { get; set; }

		public EducationWithGroupDto()
		{
			Groups = new();
		}

	}
}

