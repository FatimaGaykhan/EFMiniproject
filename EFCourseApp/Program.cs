﻿using EFCourseApp.Controllers;

EducationController educationController = new EducationController();
GroupController groupController = new GroupController();

//await educationController.Create();

//await educationController.GetAllAsync();

//await educationController.DeleteAsync();

//await educationController.GetByIdAsync();

//await educationController.UpdateAsync();

await groupController.CreateAsync();