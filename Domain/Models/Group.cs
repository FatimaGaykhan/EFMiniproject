using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Models
{
	public class Group:BaseEntity
	{
		[MaxLength(100)]
		[Required]
		public string Name { get; set; }
		[Required]
		public int Capacity { get; set; }
		public int EducationId { get; set; }
		public Education Education { get; set; }

	}
}

