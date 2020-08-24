
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Database;

public interface  IIssueRepository
{
    Task<string> CreateIssue(Issue issue);
    Task<IEnumerable<Issue>> GetAllIssues();
    Task<Issue> GetIssue(int id);
    Task<string> UpdateIssue(Issue issue);
    Task<string>DeleteIssue(int id);
    Task<List<Issue>> GetIssueUnderProject(int projectId);
    Task<Issue> GetIssueDetail(int projectId, int issueId);
    Task<string> UpdateStatus(Issue issue);
    Task<string> AssignIssue(Issue issue);
    Task<List<Issue> > SearchIssueByTitle(string title);
    Task<List<Issue> > SearchIssueByDescription(string description);
    Task<List<Issue>> FilterIssue(int issueProjectId, string issueType, string issueTitle, string issueDescription, string issueReporter, string issueAssignee, string issueStatus, int issueSprintId);
}
