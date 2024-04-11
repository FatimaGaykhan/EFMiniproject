using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Models
{
	public class Education:BaseEntity
	{
		[MaxLength(100)]
		[Required]
		public string Name { get; set; }
		[Required]
		public string Color { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}

