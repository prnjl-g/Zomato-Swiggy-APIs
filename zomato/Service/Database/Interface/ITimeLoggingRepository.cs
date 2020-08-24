
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Database;

public interface  ITimeLoggingRepository
{
    Task<string> CreateLog(TimeLogging timeLogging);
    Task<string> UpdateLog(UpdateLog logDetails);
    Task<string> DeleteLog(int logId, string user);
    Task<List<TimeLogging> > FilterTimeLog(string logCreater, long logTime, int issueId);
}
 