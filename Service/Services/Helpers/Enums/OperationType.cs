using System;
namespace Service.Services.Helpers.Enums
{
	public enum OperationType
	{
        GroupCreate = 1,
        GroupUpdate,
        GroupDelete,
        GetGroupById,
        GetAllGroups,
        FilterByEducationName,
        GetAllWithEducationId,
        SortWithCapacity,
        SearchGroupByName,
        EducationCreate,
        EducationUpdate,
        EducationDelete,
        GetEducationById,
        GetAllEducations,
        GetAllEducationWithGroups,
        SortWithCreatedDate,
        SearchByEducationName,
    }
}

