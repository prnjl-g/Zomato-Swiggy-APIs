using Service.Database;
using GraphQL;
using System;
using System.Linq;

namespace Service.Graphql 
{
  [GraphQLMetadata("Mutation")]
  public class Mutation 
  {
    [GraphQLMetadata("addIssue")]
    public Issue AddIssue(int issueProjectId, string issueType, string issueTitle, string issueDescription, string issueReporter, string issueAssignee, string issueStatus, int issueSprintId)
    {
      using(var db = new StoreContext()) 
      {
        var project = db.ProjectDetails.Find(issueProjectId);
        if(project == null)
        {
          return null;
        }
        var newIssue = new Issue{issueProjectId= issueProjectId, issueType=issueType, issueSprintId = issueSprintId, issueStatus=issueStatus, issueDescription= issueDescription, issueAssignee= issueAssignee, issueReporter = issueReporter, issueTitle = issueTitle};
        db.IssueList.Add(newIssue);
        db.SaveChanges();
        return newIssue;
      }
    }

    [GraphQLMetadata("addProject")]
    public Project AddProject(string projectDescription, string createrOfProject)
    {
      using(var db = new StoreContext()) 
      {
        var newProject = new Project{projectDescription= projectDescription, creatorOfProject=createrOfProject};
        db.ProjectDetails.Add(newProject);
        db.SaveChanges();
        return newProject;
      }
    }


    [GraphQLMetadata("addSprint")]
    public Sprint AddSprint(int projectId, DateTime sprintStartDate, DateTime sprintEndDate, string sprintStatus)
    {
      using(var db = new StoreContext()) 
      {
        var project = db.ProjectDetails.Find(projectId);
        if(project == null)
        {
          return null;
        }
        var newSprint = new Sprint{projectId= projectId, sprintStartDate=sprintStartDate, sprintEndDate = sprintEndDate, sprintStatus=sprintStatus};
        db.Sprints.Add(newSprint);
        db.SaveChanges();
        return newSprint;
      }
    }


    [GraphQLMetadata("addTimeLog")]
    public TimeLogging AddTimeLog(string logCreater, int logTime, int issueId)
    {
      using(var db = new StoreContext()) 
      {
        var issue = db.IssueList.Find(issueId);
        if(issue == null)
        {
          return null;
        }
        var newTimeLog = new TimeLogging{logCreater= logCreater, logTime=logTime, issueId = issueId};
        db.TimeLoggings.Add(newTimeLog);
        db.SaveChanges();
        return newTimeLog;
      }
    }



    [GraphQLMetadata("addLabel")]
    public Label AddLabel(string label, int issueId)
    {
      using(var db = new StoreContext()) 
      {
        var issue = db.IssueList.Find(issueId);
        if(issue == null)
        {
          return null;
        }
        var newLabel = new Label{label= label, issueId=issueId};
        db.Labels.Add(newLabel);
        db.SaveChanges();
        return newLabel;
      }
    }



    [GraphQLMetadata("addComment")]
    public Comment AddComment(int issueId, string comment, string userName)
    {
      using(var db = new StoreContext()) 
      {
        var issue = db.IssueList.Find(issueId);
        if(issue == null)
        {
          return null;
        }
        var newComment = new Comment{issueId= issueId, comment=comment, userName = userName};
        db.CommentsOnIssues.Add(newComment);
        db.SaveChanges();
        return newComment;
      }
    }
  }
}