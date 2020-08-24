using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Database;
using System;

public class SprintRepository : ISprintRepository
{ 
      private StoreContext this_dataBaseContext;

      public SprintRepository(StoreContext context){
            this_dataBaseContext = context;
      }

      //method to create a sprint.
      public async Task<string> CreateSprint(Sprint sprintDetails)
      {
          var project = this_dataBaseContext.ProjectDetails.Where(i => i.projectId == sprintDetails.projectId).ToList();
          if(project.Count == 0)
          {
              return await Task.FromResult("No project Exist with given projectID");
          }
          this_dataBaseContext.Sprints.Add(sprintDetails);
          this_dataBaseContext.SaveChanges();
          return await Task.FromResult("Sprint is created");
      }

      //method to start a sprint.
      public async Task<string> StartSprint(int sprintId)
      {
          var sprint = this_dataBaseContext.Sprints.Find(sprintId);
          if(sprint == null)
          {
              return await Task.FromResult("Invalid sprint ID");
          }
          if(sprint.sprintStatus == "Start")
          {
              return await Task.FromResult("Sprint is already started");
          }
          sprint.sprintStatus = "Start";
          this_dataBaseContext.SaveChanges();  
          return await Task.FromResult($"Sprint with id = {sprintId} started....");
      }

      //method to stop a sprint.
      public async Task<string> StopSprint(int sprintId)
      {
          var sprint = this_dataBaseContext.Sprints.Find(sprintId);
          if(sprint == null)
          {
              return await Task.FromResult("Invalid sprint ID");
          }
          if(sprint.sprintStatus == "Stop")
          {
              return await Task.FromResult("Sprint is already stopped");
          }
          sprint.sprintStatus = "Stop";
          this_dataBaseContext.SaveChanges(); 
          return await Task.FromResult($"Sprint with id = {sprintId} Stopped");
      }

      //method to delete a sprint.
      public async Task<string> DeleteSprint(int sprintId)
      {
          var sprint = this_dataBaseContext.Sprints.Find(sprintId);
          if(sprint == null)
          {
              return await Task.FromResult("Invalid sprint Id");
          }
          this_dataBaseContext.Sprints.Remove(sprint);
          this_dataBaseContext.SaveChanges();
          return await Task.FromResult($"Sprint with id = {sprintId} deleted.");
      }

      //method to add existing issue to the sprint.
      public async Task<string> AddIssue(int sprintId, int issueId)
      {
          var sprint = this_dataBaseContext.Sprints.Find(sprintId);
          if(sprint == null)
          {
              return await Task.FromResult("Invalid sprintId");
          }
          var issue = this_dataBaseContext.IssueList.Find(issueId);
          if(issue == null)
          {
              return await Task.FromResult("Invalid issueID");
          }
          if(issue.issueSprintId > 0)
          {
              return await Task.FromResult($"Issue with id = {issueId} is already the part of other sprint");
          }
          if(sprint.projectId != issue.issueProjectId)
          {
              return await Task.FromResult("the projectid of sprint and issue is not same");
          }
          issue.issueSprintId = sprintId;
          this_dataBaseContext.SaveChanges();
          return await Task.FromResult($"Issue with id = {issueId} is added to sprint with id = {sprintId}");
      }

      //method to add new issue to the sprint.
      public async Task<string> AddNewIssue(Issue issue)
      {
            var project = this_dataBaseContext.ProjectDetails.ToList();
            var filterProject = from Project in project
                            where Project.projectId == issue.issueProjectId 
                            select Project;
            if(filterProject.Count() == 0){
                return await Task.FromResult("Project Doesn't Exist");
            }
            this_dataBaseContext.IssueList.Add(issue);
            this_dataBaseContext.SaveChanges();
            return await Task.FromResult("New Issue Created Successfuly");
      }

      //method to remove an issue.
      public async Task<string> RemoveIssue(int sprintId, int issueId)
      {
          var issue = this_dataBaseContext.IssueList.Find(issueId);
          if(issue == null)
          {
              return await Task.FromResult("Invalid Issue Id");
          }
          if(issue.issueSprintId != sprintId)
          {
              return await Task.FromResult("Issue is not inside this sprint");
          }
          issue.issueSprintId=0;
          this_dataBaseContext.SaveChanges();  
          return await Task.FromResult($"Issue with id = {issueId} is removed from the sprint with id = {sprintId}");
      }

      //method to filter an issue on the basis of any zero level entity.
      public async Task<List<Sprint>> FilterSprint(int projectId, DateTime sprintStartDate, DateTime sprintEndDate, string sprintStatus)
      {
          var result = this_dataBaseContext.Sprints.Where(i => i.projectId == (projectId == 0 ? i.projectId : projectId) && i.sprintStatus == (sprintStatus == null ? i.sprintStatus : sprintStatus)).ToList();
          return await Task.FromResult(result);
      }
      
}