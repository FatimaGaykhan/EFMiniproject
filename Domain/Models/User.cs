using System;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Models
{
	public class User:BaseEntity
	{
		[MaxLength(100)]
        [Required]
		public string FullName { get; set; }
        [MaxLength(100)]
        [Required]
        public string Username { get; set; }
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }
        [MaxLength(50)]
        [Required]
        public string Password { get; set; }

	}
}

