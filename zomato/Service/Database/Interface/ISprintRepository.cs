
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Database;
using System;
 
public interface  ISprintRepository
{
    Task<string> CreateSprint(Sprint sprintDetails);
    Task<string> StartSprint(int sprintId);
    Task<string> StopSprint(int sprintId);
    Task<string> DeleteSprint(int sprintId);
    Task<string> AddIssue(int sprintId, int issueId);
    Task<string> AddNewIssue(Issue issue);
    Task<string> RemoveIssue(int sprintId, int issueId);
    Task<List<Sprint>> FilterSprint(int projectId, DateTime sprintStartDate, DateTime sprintEndTime, string sprintStatus);
}
