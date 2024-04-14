using EFCourseApp.Controllers;
using Service.Services.Helpers.Enums;
using Service.Services.Helpers.Extensions;

EducationController educationController = new EducationController();
GroupController groupController = new GroupController();
UserController userController = new UserController();

ConsoleColor.Cyan.WriteConsole("Choose one operation: 1-register 2-login ");
Home: string inserted = Console.ReadLine();
if (inserted=="1")
{
    await userController.RegisterAsync();
    goto Home;
}
else if (inserted == "2")
{
    await userController.LoginAsync();
    if (userController.Login)
    {
        while (true)
        {

            GetMenues();
            Operation: string insertedOperation = Console.ReadLine();
            int operation;
            bool IsCorrectOperationFormat = int.TryParse(insertedOperation, out operation);

            if (IsCorrectOperationFormat)
            {
                switch (operation)
                {
                    case (int)OperationType.EducationCreate:
                        await educationController.Create();
                        break;
                    case (int)OperationType.EducationUpdate:
                        await educationController.UpdateAsync();
                        break;
                    case (int)OperationType.EducationDelete:
                        await educationController.DeleteAsync();
                        break;
                    case (int)OperationType.GetEducationById:
                        await educationController.GetByIdAsync();
                        break;
                    case (int)OperationType.GetAllEducations:
                        await educationController.GetAllAsync();
                        break;
                    case (int)OperationType.GetAllEducationWithGroups:
                        await educationController.GetAllWithGroupsAsync();
                        break;
                    case (int)OperationType.SortWithCreatedDate:
                        await educationController.SortWithCreatedDateAsync();
                        break;
                    case (int)OperationType.SearchByEducationName:
                        await educationController.SearchByNameAsync();
                        break;
                    case (int)OperationType.GroupCreate:
                        await groupController.CreateAsync();
                        break;
                    case (int)OperationType.GroupUpdate:
                        await groupController.UpdateAsync();
                        break;
                    case (int)OperationType.GroupDelete:
                        await groupController.DeleteAsync();
                        break;
                    case (int)OperationType.GetGroupById:
                        await groupController.GetByIdAsync();
                        break;
                    case (int)OperationType.GetAllGroups:
                        await groupController.GetAllAsync();
                        break;
                    case (int)OperationType.FilterByEducationName:
                        await groupController.FilterByEducationNameAsync();
                        break;
                    case (int)OperationType.GetAllWithEducationId:
                        await groupController.GetAllWithEducationId();
                        break;
                    case (int)OperationType.SortWithCapacity:
                        await groupController.SortWithCapacityAsync();
                        break;
                    case (int)OperationType.SearchGroupByName:
                        await groupController.SearchByNameAsync();
                        break;
                    default:
                        ConsoleColor.Red.WriteConsole("Operation is wrong, please choose again");
                        goto Operation;

                }



            }
            else
            {
                ConsoleColor.Red.WriteConsole("Operation format is wrong, please add operation again");
                goto Operation;
            }

        }

    }
}
else
{
    ConsoleColor.Red.WriteConsole("Operation format is wrong, please add operation again");
    goto Home;
}



static void GetMenues()
{
    ConsoleColor.Magenta.WriteConsole("Choose One Operation: \n 1-Education Create \n 2-Education Update \n 3-Education Delete \n 4-Get Education By Id \n 5-Get All Educations \n 6-Get All Education With Groups \n 7-Sort With Created Date  \n 8-Search  By Education Name \n 9-Group Create \n 10-Group Update \n 11-Group Delete \n 12-Get Group By Id \n 13-Get All Groups \n 14-Filter By Education Name \n 15-Get All With Education Id  \n 16-Sort With Capacity \n 17-Search Group By Name ");
}

