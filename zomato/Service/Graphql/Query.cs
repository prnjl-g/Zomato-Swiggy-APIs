using System.Collections.Generic;
using GraphQL;
using System.Linq;
using Service.Database;
using Microsoft.EntityFrameworkCore;

namespace Service.Graphql 
{
  public class Query
  {

    [GraphQLMetadata("issues")]
    public IEnumerable<Issue> GetIssues()
    {
      using(var db = new StoreContext())
      {
        // implement it

        return db.IssueList.ToList();
      }
    }

    [GraphQLMetadata("project")]
    public IEnumerable<Project> GetProject()
    {
      using(var db = new StoreContext())
      {
        return db.ProjectDetails.ToList();
      }
    }

    [GraphQLMetadata("comment")]
    public IEnumerable<Comment> GetComment()
    {
      using(var db = new StoreContext())
      {
        return db.CommentsOnIssues.ToList();
      }
    }

    [GraphQLMetadata("label")]
    public IEnumerable<Label> GetLabel()
    {
      using(var db = new StoreContext())
      {
        return db.Labels.ToList();
      }
    }

    [GraphQLMetadata("sprint")]
    public IEnumerable<Sprint> GetSprint()
    {
      using(var db = new StoreContext())
      {
        return db.Sprints.ToList();
      }
    }

    [GraphQLMetadata("timeLogging")]
    public IEnumerable<TimeLogging> GetTimeLogging()
    {
      using(var db = new StoreContext())
      {
        return db.TimeLoggings.ToList();
      }
    }
  }
}