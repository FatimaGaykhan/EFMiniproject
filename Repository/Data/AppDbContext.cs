using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
	public class AppDbContext:DbContext
	{
        public DbSet<Group> Groups { get; set; } = null;
        public DbSet<Education> Educations { get; set; } = null;
        public DbSet<User> Users { get; set; } = null;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=EFCourseApp;User Id=sa;Password=Salam123");
        }
    }
}

