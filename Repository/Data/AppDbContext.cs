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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Education>().HasData(
                new Education { Id=1000,Name="Back End",Color="orange",CreatedDate=DateTime.Now},
                new Education { Id=1001,Name="Front End",Color="green", CreatedDate=DateTime.Now},
                new Education { Id=1002,Name="Cyber Security", Color="black", CreatedDate=DateTime.Now}
                );
            modelBuilder.Entity<Group>().HasData(
                new Group { Id=30,Name="Group100",Capacity=20,EducationId=1000, CreatedDate=DateTime.Now},
                new Group { Id = 32, Name = "Group105", Capacity = 15, EducationId = 1001, CreatedDate = DateTime.Now },
                new Group { Id = 33, Name = "Group106", Capacity = 20, EducationId = 1002, CreatedDate = DateTime.Now },
                new Group { Id = 34, Name = "Group107", Capacity = 18, EducationId = 1002, CreatedDate = DateTime.Now }
                );

            
            base.OnModelCreating(modelBuilder);
        }
    }
}

