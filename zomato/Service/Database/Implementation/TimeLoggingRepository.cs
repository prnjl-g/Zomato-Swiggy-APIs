using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Database;

public class TimeLoggingRepository : ITimeLoggingRepository
{
      private StoreContext this_dataBaseContext;

      public TimeLoggingRepository(StoreContext context){
            this_dataBaseContext = context;
      }

    //method to create a log.
     public async Task<string> CreateLog(TimeLogging timeLogging)
     {
         var issue = this_dataBaseContext.IssueList.Where(i => i.issueId == timeLogging.issueId).ToList();
         if(issue.Count == 0)
         {
             return await Task.FromResult("Invalid issue id");
         }
         this_dataBaseContext.TimeLoggings.Add(timeLogging);
         this_dataBaseContext.SaveChanges();
         return await Task.FromResult($"New Time log created for issue with id = {timeLogging.issueId}.");
     }

    //method to update a log.
     public async Task<string>UpdateLog(UpdateLog logDetails)
     {
         var log = this_dataBaseContext.TimeLoggings.Find(logDetails.logId);
         if(log == null)
         {
             return await Task.FromResult("Invalid log id");
         }
         if(log.logCreater != logDetails.logUpdater)
         {
             return await Task.FromResult("You are not authroized to update this log.");
         }
         log.logTime=logDetails.updatedTime;
         this_dataBaseContext.SaveChanges();
         return await Task.FromResult($"Log with id {logDetails.logId} is updated");
     }
      
    //method to selete a log.
     public async Task<string> DeleteLog(int logId, string user) 
     {
         var log = this_dataBaseContext.TimeLoggings.Find(logId);
         if(log == null)
         {
             return await Task.FromResult("Invalid log id");
         }
         if(log.logCreater != user)
         {
             return await Task.FromResult("You are not authroized to delete this log.");
         }
         this_dataBaseContext.TimeLoggings.Remove(log);
         this_dataBaseContext.SaveChanges();
         return await Task.FromResult($"Log with id = {logId} is deleted");
     }

    //method to filter time logs on the basis of any zero level entity.
     public async Task<List<TimeLogging>> FilterTimeLog(string logCreater, long logTime, int issueId)
     {
         var result = this_dataBaseContext.TimeLoggings.Where(i => i.logCreater == (logCreater == null ? i.logCreater : logCreater) && i.logTime == (logTime == 0 ? i.logTime : logTime) && i.issueId == (issueId == 0 ? i.issueId : issueId)).ToList();
         return await Task.FromResult(result);
     }
      
}