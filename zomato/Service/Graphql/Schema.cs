using GraphQL.Types;
using GraphQL;
using Service.Database;

namespace Service.Graphql 
{
  public class MySchema 
  {
    private ISchema _schema { get; set; }
    public ISchema GraphQLSchema 
    {  
      get 
      {
        return this._schema;
      }
    }
    
    public MySchema() 
    {
      this._schema = Schema.For(@"
          type Issue {
            issueId : ID
            issueProjectId: Int,
            issueType:String,
            issueTitle:String,
            issueDescription:String,
            issueReporter : String,
            issueAssignee:String,
            issueStatus:String,
            issueSprintId:Int
          }


          type Project {
            projectId : ID
            projectDescription : String,
            creatorOfProject : String
          }

          type Comment {
            commentId : ID
            issueId : Int,
            comment : String,
            userName : String
          }

          type Label {
            labelId : ID
            label : String,
            issueId : Int
          }

          type Sprint {
            sprintId : ID
            projectId : Int,
            sprintStartDate : DateTime,
            sprintEndDate : DateTime,
            sprintStatus : String
          }

          type TimeLogging{
            logId : ID
            logCreator : String,
            logTime : Int,
            issueId : Int
          }


          input IssueInput {
            issueId : Int
            issuetitle: String
          }


           type Query {
              issues: [Issue]
              project : [Project]
              comment : [Comment]
              label : [Label]
              sprint : [Sprint]
              timeLogging : [TimeLogging]
          }


          type Mutation {
             addIssue(issueProjectId : Int, issueType : String, issueTitle : String, issueDescription : String, issueReporter : String, issueAssignee : String, issueStatus : String, issueSprintId : Int): String
             addProject(projectDescription : String, creatorOfProject : String) : String
             addSprint(projectId : Int, sprintStartDate : DateTime, sprintEndDate : DateTime, sprintStatus : String)  : String
             addTimeLog(logCreater : String, logTime : Int, issueId : Int) :String
             addLabel(label : String, issueId : Int) : String
             addComment(issueId : Int, comment : String, userName : String) : String
          }

      ", _ =>
      {
        _.Types.Include<Query>();
        _.Types.Include<Mutation>();
      });
    }

  }
}