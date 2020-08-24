using System.ComponentModel.DataAnnotations;

namespace Service.Database
{
    public class TimeLogging
    {
        [Key]
        public int logId{get; set;}
        public string logCreater{get; set;}
        public long logTime{get; set;}
        [Required]
        public int issueId{get; set;}
    }
 
    public class UpdateLog
    {
        [Key]
        public int logId{get; set;}
        
        public string logUpdater{get; set;}
        public long updatedTime{get; set;}

    }
}
