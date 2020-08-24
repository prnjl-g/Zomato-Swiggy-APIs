using System.ComponentModel.DataAnnotations;

namespace Service.Database
{
    public class Issue
    {
        public int issueId {get;set;}
        [Required]
        public int issueProjectId{get; set;}
        public string issueType{get; set;}
        public string issueTitle{get; set;}
        public string issueDescription {get; set;}
        public string issueReporter{get; set;}
        public string issueAssignee{get; set;}
        [Required]
        public string issueStatus{get; set;}
        public int issueSprintId{get; set;}
    }
}