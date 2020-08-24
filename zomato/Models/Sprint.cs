using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Service.Database
{
    public class Sprint
    {
        public int sprintId{get; set;}
        [Required]
        public int projectId{get; set;}
        [Column(TypeName="date")]
        public DateTime sprintStartDate{get; set;}
        [Column(TypeName="date")]
        public DateTime sprintEndDate{get; set;}
        public List<Issue> issuesUnderSprint{get; set;}
        public string sprintStatus{get; set;}
    }
}