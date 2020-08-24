using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Database;
using System;
 
public class IssueRepository : IIssueRepository
{
      private StoreContext this_dataBaseContext;

      string[] status = new string[6]{"Open", "InProgress", "InReview", "CodeComplete", "Qa Testing", "Done"};
      public IssueRepository(StoreContext context){
            this_dataBaseContext = context;
      }

      //method to create an issue.
      public async Task<string> CreateIssue(Issue issue)
      {
            try{
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
            catch(Exception e){
                  return await Task.FromResult(e.Message);
            }
      }

      //method to get an issue.
      public async Task<Issue> GetIssue(int id){
            try
            {
                  var requiredIssue = this_dataBaseContext.IssueList.Find(id);
                  return await Task.FromResult(requiredIssue);
            }
            catch(Exception)
            {
                  return await Task.FromResult(new Issue{});
            }
      }

      //method to get all the issues.
      public async Task<IEnumerable<Issue>> GetAllIssues()
      {
            var listOfAllIssues = this_dataBaseContext.IssueList.ToList();
            return await Task.FromResult(listOfAllIssues);

      }

      //method to update a issue.
      public async Task<string> UpdateIssue(Issue issue)
      {
            var requiredIssue = this_dataBaseContext.IssueList.Find(issue.issueId);
            if(requiredIssue == null)
            {
                  return await Task.FromResult("Issue does not exist!");
            }
            if(issue.issueAssignee != null)
            {
                  requiredIssue.issueAssignee = issue.issueAssignee;
            }
            if(issue.issueDescription != null)
            {
                  requiredIssue.issueDescription = issue.issueDescription;
            }
            if(issue.issueReporter != null)
            {
                  return await Task.FromResult("Error! Reporter Can Not be change.");
            }
            if(issue.issueStatus != null)
            {
                  int ind1 = Array.IndexOf(status, issue.issueStatus);
                  int ind2 = Array.IndexOf(status, requiredIssue.issueStatus);
                  if(ind1 - ind2 >= 2)
                  {
                        return await Task.FromResult($"Error! Status Can not be change form {requiredIssue.issueStatus} to {issue.issueStatus}.");
                  }
                  requiredIssue.issueStatus = issue.issueStatus;
            }
            if(issue.issueTitle != null)
            {
                  requiredIssue.issueTitle = issue.issueTitle;
            }
            if(issue.issueType != null)
            {
                  requiredIssue.issueType = issue.issueType;
            }
            if(issue.issueProjectId != 0 && issue.issueProjectId != requiredIssue.issueProjectId)
            {
                  return await Task.FromResult("Error! ProjectId Can Not be change.");
            }
            this_dataBaseContext.SaveChanges();
            return await Task.FromResult($"Issue with id {issue.issueId} updated successfully ");
      }


      //method to delete a issue.
      public async Task<string> DeleteIssue(int id)
      {
            var requiredIssue = this_dataBaseContext.IssueList.Find(id);
            this_dataBaseContext.IssueList.Remove(requiredIssue);
            this_dataBaseContext.SaveChanges();
            return await Task.FromResult($"Issue with id = {id} is removed from the database. ");
      }
      
      //method to get issue under a project.
      public async Task<List<Issue> > GetIssueUnderProject(int projectId)
      {
            var allIssues = this_dataBaseContext.IssueList.Where(i => i.issueProjectId == projectId).ToList();
            return await Task.FromResult(allIssues);
      }

      //method to get issue details under a project.
      public async Task<Issue> GetIssueDetail(int projectId, int issueId)
      {
            var issue = this_dataBaseContext.IssueList.Where(i => i.issueProjectId == projectId && i.issueId == issueId).FirstOrDefault();
            return await Task.FromResult(issue);
      }

      //method to update status of an issue.
      public async Task<string> UpdateStatus(Issue issue)
      {
            var requiredIssue = this_dataBaseContext.IssueList.Find(issue.issueId);
            int ind1 = Array.IndexOf(status, issue.issueStatus);
            int ind2 = Array.IndexOf(status, requiredIssue.issueStatus);
            if(ind1 - ind2 >= 2)
            {
                  return await Task.FromResult($"Error! Status Can not be change form {requiredIssue.issueStatus} to {issue.issueStatus}.");
            }
            requiredIssue.issueStatus = issue.issueStatus;
            this_dataBaseContext.SaveChanges();
            return await Task.FromResult($"Status of Issue with id = {issue.issueId} is changed from {requiredIssue.issueProjectId} to {issue.issueProjectId}");
      }

      //method to assign the issue.
      public async Task<string> AssignIssue(Issue issue)
      {
            var requiredIssue = this_dataBaseContext.IssueList.Find(issue.issueId);
            if(requiredIssue == null)
            {
                  return await Task.FromResult("Issue does not exist");
            }
            requiredIssue.issueAssignee = issue.issueAssignee;
            this_dataBaseContext.SaveChanges();
            return await Task.FromResult($"Assignee of Issue with id = {issue.issueId} is changed from {requiredIssue.issueAssignee} to {issue.issueAssignee}");
      }

      //method to search issue by title.
      public async Task<List<Issue>> SearchIssueByTitle(string title)
      {
            var searchResult = this_dataBaseContext.IssueList.Where(i => i.issueTitle.Contains(title)).ToList();
            return await Task.FromResult(searchResult);
      }

      //method to search issue by description
      public async Task<List<Issue> > SearchIssueByDescription(string description)
      {
            var searchResult = this_dataBaseContext.IssueList.Where(i => i.issueDescription.Contains(description)).ToList();
            return await Task.FromResult(searchResult);
      }

      //method to filter the issues on the basis of any zero level entities.
      public async Task<List<Issue>> FilterIssue(int issueProjectId, string issueType, string issueTitle, string issueDescription, string issueReporter, string issueAssignee, string issueStatus, int issueSprintId)
      {
            var result = this_dataBaseContext.IssueList.Where(i => i.issueProjectId == (issueProjectId == 0 ? i.issueProjectId: issueProjectId) && i.issueType == (issueType == null ? i.issueType : issueType) && i.issueTitle == (issueTitle == null ? i.issueTitle : issueTitle) && i.issueDescription == (issueDescription == null ? i.issueDescription : issueDescription) && i.issueReporter == (issueReporter == null ? i.issueReporter : issueReporter) && i.issueAssignee == (issueAssignee == null ? i.issueAssignee : issueAssignee) && i.issueStatus == (issueStatus == null ? i.issueStatus : issueStatus) && i.issueSprintId == (issueSprintId == 0 ? i.issueSprintId : issueSprintId)).ToList();
            return await Task.FromResult(result);
      }
}